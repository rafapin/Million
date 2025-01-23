"use client";

import { useActionState } from "react";
import { useState } from "react";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { Label } from "@/components/ui/label";
import { Card, CardHeader, CardTitle, CardContent, CardFooter } from "@/components/ui/card";
import { createProperty } from "@/features/properties/server/action";
import { useRouter } from "next/navigation";
import { useEffect } from "react";

export default function CreatePropertyForm() {
  const initialState = { message: "", success: false };
  const [state, formAction, pending] = useActionState(createProperty, initialState);
  const [imagePreview, setImagePreview] = useState<string | null>(null);
  const router = useRouter(); // Inicializa el router

  useEffect(() => {
    if (state.success) {
      router.push("/properties"); // Redirige cuando la acción sea exitosa
    }
  }, [state.success, router]);
  const handleImageChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (file) {
      setImagePreview(URL.createObjectURL(file)); // Genera una URL para la previsualización
    }
  };

  return (
    <Card className="max-w-4xl mx-auto shadow-md">
      <CardHeader>
        <CardTitle className="text-xl font-bold">Create Property</CardTitle>
      </CardHeader>
      <form action={formAction} className="flex flex-col lg:flex-row lg:space-x-6">
        {/* Formulario */}
        <CardContent className="flex-1 space-y-4">
          {/* Name */}
          <div className="grid gap-1">
            <Label htmlFor="name">Name</Label>
            <Input id="name" name="name" placeholder="Enter property name" required />
          </div>

          {/* Address */}
          <div className="grid gap-1">
            <Label htmlFor="address">Address</Label>
            <Input id="address" name="address" placeholder="Enter property address" required />
          </div>

          {/* Price */}
          <div className="grid gap-1">
            <Label htmlFor="price">Price</Label>
            <Input
              id="price"
              name="price"
              type="number"
              placeholder="Enter property price"
              required
            />
          </div>

          {/* Code Internal */}
          <div className="grid gap-1">
            <Label htmlFor="codeInternal">Code Internal</Label>
            <Input
              id="codeInternal"
              name="codeInternal"
              placeholder="Enter internal code"
              required
            />
          </div>

          {/* Year */}
          <div className="grid gap-1">
            <Label htmlFor="year">Year</Label>
            <Input id="year" name="year" type="number" placeholder="Enter year" required />
          </div>
        </CardContent>

        {/* Imagen */}
        <CardContent className="flex-1 space-y-4">
          <div className="grid gap-1">
            <Label htmlFor="image">Image</Label>
            <Input
              id="image"
              name="image"
              type="file"
              accept="image/*"
              onChange={handleImageChange}
            />
            {imagePreview && (
              <div className="mt-4">
                <img
                  src={imagePreview}
                  alt="Preview"
                  className="max-w-full max-h-64 rounded border shadow-md"
                />
              </div>
            )}
          </div>
        </CardContent>

        {/* Botón de Envío */}
        <CardFooter className="lg:col-span-2 flex flex-col items-center space-y-2">
          <Button type="submit" disabled={pending} className="w-full lg:w-auto">
            {pending ? "Creating..." : "Create Property"}
          </Button>
          <p
            aria-live="polite"
            className={`text-sm ${
              state.success ? "text-green-600" : "text-red-600"
            }`}
          >
            {state.message}
          </p>
        </CardFooter>
      </form>
    </Card>
  );
}
