import Link from "next/link";

export default function Navbar() {
  const navItems = [
    { name: "Inicio", href: "/" },
    { name: "Properties", href: "/properties" },
    { name: "Contactos", href: "/contact" },
  ];

  return (
    <nav className="bg-gray-800 text-white fixed top-0 left-0 right-0 z-50">
      <div className="container mx-auto flex items-center justify-between p-4">
        {/* Logo o Marca */}
        <div className="text-2xl font-bold">Million</div>

        {/* Links de Navegación */}
        <ul className="flex space-x-4">
          {navItems.map((item) => (
            <li key={item.name}>
              <Link
                href={item.href}
                className="px-3 py-2 rounded hover:bg-gray-700"
              >
                {item.name}
              </Link>
            </li>
          ))}
        </ul>
      </div>
    </nav>
  );
};
