"use client";

import { Button } from "@/components/ui/button";
import Link from "next/link";

type PropertyTableProps = {
  list: PropertyListResponse[];
};

export default function PropertyTable({
  list,
}: PropertyTableProps) {
  
  return (
    <div className="overflow-x-auto">
      <table className="min-w-full border-collapse border border-gray-300 text-sm">
        <thead>
          <tr className="bg-gray-100 text-gray-700">
            <th className="border border-gray-300 px-4 py-2 text-left">Name</th>
            <th className="border border-gray-300 px-4 py-2 text-left">Address</th>
            <th className="border border-gray-300 px-4 py-2 text-right">Price</th>
            <th className="border border-gray-300 px-4 py-2 text-left">Code Internal</th>
            <th className="border border-gray-300 px-4 py-2 text-center">Year</th>
            <th className="border border-gray-300 px-4 py-2 text-center">Options</th>
          </tr>
        </thead>
        <tbody>
          {list.map((property, index) => (
            <tr
              key={index}
              className={`${
                index % 2 === 0 ? "bg-white" : "bg-gray-50"
              } hover:bg-gray-100`}
            >
              <td className="border border-gray-300 px-4 py-2">{property.name}</td>
              <td className="border border-gray-300 px-4 py-2">{property.address}</td>
              <td className="border border-gray-300 px-4 py-2 text-right">
                ${property.price.toLocaleString()}
              </td>
              <td className="border border-gray-300 px-4 py-2">{property.codeInternal}</td>
              <td className="border border-gray-300 px-4 py-2 text-center">{property.year}</td>
              <td className="border border-gray-300 px-4 py-2 text-center">
                <div className="flex justify-center space-x-2">
                  <Button
                  >
                    <Link href={"/properties/"+property.id+"/view"}>Detail</Link>
                  </Button>
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
