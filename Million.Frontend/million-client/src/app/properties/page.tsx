import { Button } from "@/components/ui/button";
import PropertyTable from "@/features/properties/components/property-table";
import { getPropertiesList } from "@/features/properties/server/properties-list";
import Link from "next/link";


export default async function Page() {
  const properties = await getPropertiesList();
  return (
    <div className="m-12">
      {/* Encabezado con Título y Botón */}
      <div className="flex justify-between items-center mb-6 pt-10">
        <h1 className="text-3xl font-bold text-gray-800">Properties</h1>
        <Button >
          <Link href="/properties/create">
              Create
          </Link>
        </Button>
      </div>

      {/* Tabla de Propiedades */}
      <PropertyTable list={properties.data} />
    </div>

  );
}
