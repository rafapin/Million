const BASE_URL = process.env.API_URL ?? '';
  
const handleResponse = async <T>(response: Response): Promise<ApiResponse<T>> => {
    if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
    }
    return response.json();
};

// GET request
export const apiGet = async <T>(endpoint: string): Promise<ApiResponse<T>> => {
    try {
        const response = await fetch(`${BASE_URL}/${endpoint}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        },
        });

        return handleResponse<T>(response);
    } catch (error) {
        console.error('Fetch GET Error:', error);
        throw error;
    }
};

// POST request
export const apiPost = async <T, B>(endpoint: string, body: B): Promise<ApiResponse<T>> => {
    try {
        const response = await fetch(`${BASE_URL}/${endpoint}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(body),
        });

        return handleResponse<T>(response);
    } catch (error) {
        console.error('Fetch POST Error:', error);
        throw error;
    }
};

// Método POST para enviar FormData
export const apiPostFormData = async <T>(endpoint: string, formData: FormData): Promise<ApiResponse<T>> => {
    try {
        const response = await fetch(`${BASE_URL}/${endpoint}`, {
            method: "POST",
            body: formData, // Envía el FormData directamente
        });

        return handleResponse<T>(response);
    } catch (error) {
        console.error("Fetch POST FormData Error:", error);
        throw error;
    }
};


// PUT request
export const apiPut = async <T, B>(endpoint: string, body: B): Promise<ApiResponse<T>> => {
    try {
        const response = await fetch(`${BASE_URL}${endpoint}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(body),
        });

        return handleResponse<T>(response);
    } catch (error) {
        console.error('Fetch PUT Error:', error);
        throw error;
    }
};

// DELETE request
export const apiDelete = async <T>(endpoint: string): Promise<ApiResponse<T>> => {
    try {
        const response = await fetch(`${BASE_URL}${endpoint}`, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
        },
        });

        return handleResponse<T>(response);
    } catch (error) {
        console.error('Fetch DELETE Error:', error);
        throw error;
    }
};
  