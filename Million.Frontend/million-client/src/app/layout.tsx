import type { Metadata } from "next";
import { ReactNode } from "react";
import { Roboto } from "next/font/google";
import { clsx } from "clsx";
import "./globals.css";
import Navbar from "@/components/navbar";

const roboto = Roboto({
  weight: ["400", "500", "700"],
  style: "normal",
  subsets: ["latin"],
  display: "swap",
});

export const metadata: Metadata = {
  title: "Million",
  description: "Sell platform for people who want luxury lives",
};

type RootLayoutProps = Readonly<{ children: ReactNode }>;

export default async function RootLayout({ children }: RootLayoutProps) {
  return (
    <html lang="en">
      <body className={clsx("antialiased", roboto.className)}>
        {/* Navbar */}
        <Navbar />

        {/* Contenido Principal Centrado */}
        <div className="flex flex-col items-center justify-center h-screen">
          {/* Asegura espacio para la barra fija */}
          {children}
        </div>
      </body>
    </html>
  );
}
