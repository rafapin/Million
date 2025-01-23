"use server";

import { propertyCreate } from "@/features/properties/server/property-create";
import { revalidatePath } from "next/cache";
import { redirect } from "next/navigation";
import { z } from "zod";

// Define el esquema de validación con Zod
const PropertySchema = z.object({
  name: z.string().min(1, "Name is required"),
  address: z.string().min(1, "Address is required"),
  price: z
    .number({ invalid_type_error: "Price must be a number" })
    .min(0, "Price must be greater than or equal to 0"),
  codeInternal: z.string().min(1, "Code Internal is required"),
  year: z
    .number({ invalid_type_error: "Year must be a number" })
    .min(1900, "Year must be greater than or equal to 1900")
    .max(new Date().getFullYear(), "Year cannot be in the future"),
  idOwner: z.string().optional(),
});

// Tipo inferido para TypeScript

export async function createProperty(
  state: { message: string, success: boolean},
  payload: FormData
) {
  try {
    // Convertimos FormData a un objeto plano
    const rawData = Object.fromEntries(payload.entries());

    // Validamos los datos usando Zod
    const property = PropertySchema.parse({
      name: rawData.name as string,
      address: rawData.address as string,
      price: Number(rawData.price),
      codeInternal: rawData.codeInternal as string,
      year: Number(rawData.year),
      idOwner: "test"
    });

    // Llamamos a la función para crear la propiedad
    await propertyCreate(payload);

    // Revalidamos la página
    revalidatePath("/properties");

    return {
      message: "Property created successfully!",
      success: true
    };
  } catch (error) {
    if (error instanceof z.ZodError) {
      // Manejo de errores de validación de Zod
      return {
        message: error.errors[0].message, // Devuelve el primer mensaje de error
        success: false
      };
    }

    // Manejo de errores inesperados
    console.error("Unexpected error:", error);
    return {
      message: "An unexpected error occurred.",
      success: false
    };
  }
}
