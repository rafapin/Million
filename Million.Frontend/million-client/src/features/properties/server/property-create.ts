import { apiPostFormData } from "@/lib/api-service";

export async function propertyCreate(request:FormData){
    return apiPostFormData<Boolean>("Property",request);
}