using AutoMapper;
using Moq;
using System.Linq.Expressions;
using Million.Application.Features.Properties.Queries.GetProperties;
using Million.Application.Features.Properties.ViewModels;
using Million.Domain.Contracts;
using Million.Domain.Features.Properties.Entities;

namespace Million.Tests.Features.Properties.Queries
{
    [TestFixture]
    public class GetPropertiesQueryHandlerTests
    {
        private Mock<IGenericRepository<Property>> _repositoryMock;
        private Mock<IMapper> _mapperMock;
        private GetPropertiesQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<IGenericRepository<Property>>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetPropertiesQueryHandler(_repositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task Handle_WhenCalled_ReturnsMappedProperties()
        {
            // Arrange
            var query = new GetPropertiesQuery { Name = "Test Property", Address = "123 Street" };

            var properties = new List<Property>
            {
                new Property { Name = "Test Property", Address = "123 Street" },
                new Property { Name = "Another Property", Address = "456 Avenue" }
            };

            _repositoryMock
                .Setup(repo => repo.GetAsync(
                    It.IsAny<Expression<Func<Property, bool>>>(),
                    It.IsAny<Func<IQueryable<Property>, IOrderedQueryable<Property>>>(),
                    It.IsAny<List<Expression<Func<Property, object>>>>(),
                    It.IsAny<bool>()
                ))
                .ReturnsAsync((Expression<Func<Property, bool>> predicate,
                               Func<IQueryable<Property>, IOrderedQueryable<Property>>? orderBy,
                               List<Expression<Func<Property, object>>>? includes,
                               bool disableTracking) =>
                {
                    return properties.AsQueryable().Where(predicate).ToList();
                });

            var propertyVms = new List<PropertyVm>
            {
                new PropertyVm { Name = "Test Property", Address = "123 Street" },
                new PropertyVm { Name = "Another Property", Address = "456 Avenue" }
            };

            _mapperMock
                .Setup(mapper => mapper.Map<List<PropertyVm>>(It.IsAny<List<Property>>()))
                .Returns(propertyVms);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Test Property", result[0].Name);
            Assert.AreEqual("123 Street", result[0].Address);

            _repositoryMock.Verify(repo => repo.GetAsync(
                It.IsAny<Expression<Func<Property, bool>>>(),
                It.IsAny<Func<IQueryable<Property>, IOrderedQueryable<Property>>>(),
                It.IsAny<List<Expression<Func<Property, object>>>>(),
                It.IsAny<bool>()
            ), Times.Once);

            _mapperMock.Verify(mapper => mapper.Map<List<PropertyVm>>(It.IsAny<List<Property>>()), Times.Once);
        }

        [Test]
        public async Task Handle_WithNoMatchingProperties_ReturnsEmptyList()
        {
            // Arrange
            var query = new GetPropertiesQuery { Name = "Nonexistent", Address = "" };

            _repositoryMock
                .Setup(repo => repo.GetAsync(
                    It.IsAny<Expression<Func<Property, bool>>>(),
                    It.IsAny<Func<IQueryable<Property>, IOrderedQueryable<Property>>>(),
                    It.IsAny<List<Expression<Func<Property, object>>>>(),
                    It.IsAny<bool>()
                ))
                .ReturnsAsync(new List<Property>());

            _mapperMock
                .Setup(mapper => mapper.Map<List<PropertyVm>>(It.IsAny<List<Property>>()))
                .Returns(new List<PropertyVm>());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);

            _repositoryMock.Verify(repo => repo.GetAsync(
                It.IsAny<Expression<Func<Property, bool>>>(),
                It.IsAny<Func<IQueryable<Property>, IOrderedQueryable<Property>>>(),
                It.IsAny<List<Expression<Func<Property, object>>>>(),
                It.IsAny<bool>()
            ), Times.Once);

            _mapperMock.Verify(mapper => mapper.Map<List<PropertyVm>>(It.IsAny<List<Property>>()), Times.Once);
        }

        [Test]
        public async Task Handle_WithIncludes_ReturnsPropertiesWithIncludes()
        {
            // Arrange
            var query = new GetPropertiesQuery { Name = "Test Property", Address = "123 Street" };

            var properties = new List<Property>
            {
                new Property { Name = "Test Property", Address = "123 Street" }
            };

            _repositoryMock
                .Setup(repo => repo.GetAsync(
                    It.IsAny<Expression<Func<Property, bool>>>(),
                    It.IsAny<Func<IQueryable<Property>, IOrderedQueryable<Property>>>(),
                    It.IsAny<List<Expression<Func<Property, object>>>>(),
                    It.IsAny<bool>()
                ))
                .ReturnsAsync(properties);

            var propertyVms = new List<PropertyVm>
            {
                new PropertyVm { Name = "Test Property", Address = "123 Street" }
            };

            _mapperMock
                .Setup(mapper => mapper.Map<List<PropertyVm>>(properties))
                .Returns(propertyVms);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Test Property", result[0].Name);
            Assert.AreEqual("123 Street", result[0].Address);

            _repositoryMock.Verify(repo => repo.GetAsync(
                It.IsAny<Expression<Func<Property, bool>>>(),
                It.IsAny<Func<IQueryable<Property>, IOrderedQueryable<Property>>>(),
                It.IsAny<List<Expression<Func<Property, object>>>>(),
                It.IsAny<bool>()
            ), Times.Once);

            _mapperMock.Verify(mapper => mapper.Map<List<PropertyVm>>(properties), Times.Once);
        }
    }
}
