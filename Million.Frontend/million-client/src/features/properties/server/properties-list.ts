import { apiGet } from "@/lib/api-service";

export async function getPropertiesList(){
    return apiGet<PropertyListResponse[]>("Property");
}