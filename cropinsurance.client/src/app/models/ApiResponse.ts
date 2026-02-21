export interface ApiResponse {
    status: "Success" | "Error" | "Exception";
    message?: string;
    error?: string;
    newId?: number;
}