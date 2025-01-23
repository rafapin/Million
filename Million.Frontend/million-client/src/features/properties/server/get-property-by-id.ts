import { apiGet } from "@/lib/api-service";

export async function getPropertyById(id:string){
    return apiGet<PropertyDetailResponse>("Property/"+id);
}