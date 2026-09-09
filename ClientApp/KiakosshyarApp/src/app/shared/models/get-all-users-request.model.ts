export interface PaginationRequest {
  pageNumber: number;
  pageSize: number;
  sortBy?: string;
  sortDescending: boolean;
}

export interface GetAllUsersRequest {
  searchKey?: string;
  paginationRequest: PaginationRequest;
}