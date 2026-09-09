export interface ApiResponse<T = any> {
  isSuccess: boolean;
  message?: string;
  data: T;
  statusCode: number;
}