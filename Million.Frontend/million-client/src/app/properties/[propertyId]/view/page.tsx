import { Button } from "@/components/ui/button";
import { Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle } from "@/components/ui/card";
import PropertyForm from "@/features/properties/components/property-form";
import { getPropertyById } from "@/features/properties/server/get-property-by-id";
import Link from "next/link";

type PageProps = {
  params: Promise<{propertyId: string}>;
};
export default async function Page({params}:PageProps) {
  const {propertyId} = await params;
  const property = (await getPropertyById(propertyId)).data;
  return (
    <Card className="max-w-lg mx-auto shadow-md">
      {/* Imagen */}
      <CardHeader className="p-0">
        <img
          src={`data:image/jpeg;base64,${property.image}`}
          alt={property.name}
          className="w-full h-48 object-cover rounded-t"
        />
      </CardHeader>

      {/* Información de la Propiedad */}
      <CardContent className="space-y-2">
        <CardTitle className="text-2xl font-bold">{property.name}</CardTitle>
        <CardDescription className="text-sm text-muted-foreground">
          {property.address}
        </CardDescription>

        <div className="space-y-1">
          <p>
            <span className="font-semibold">Price:</span> ${property.price.toLocaleString()}
          </p>
          <p>
            <span className="font-semibold">Code Internal:</span> {property.codeInternal}
          </p>
          <p>
            <span className="font-semibold">Year:</span> {property.year}
          </p>
        </div>
      </CardContent>

      {/* Pie de Tarjeta */}
      <CardFooter>
        <Button >
          <Link href="/properties">Go back</Link>
        </Button>
      </CardFooter>
    </Card>
  );
}
