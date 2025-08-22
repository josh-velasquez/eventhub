export type RequestStatus = "idle" | "pending" | "fulfilled" | "failed";

export type Sorting = "asc" | "desc";

export interface EventsState {
  events: EventsResponse | null;
  status: RequestStatus;
  error: string | null;
}

export interface Event {
  id: string;
  name: string;
  startsOn: string;
  endsOn: string;
  location: string;
}

export interface EventsResponse {
  events: Event[];
  totalCount: number;
  totalPages: number;
  currentPage: number;
  pageSize: number;
}

export interface EventsQuery {
  startDate: string;
  endDate: string;
  page: number;
  pageSize: number;
  sortBy: string;
  sortOrder: Sorting;
  searchQuery: string;
}

export interface SalesState {
  topEventsBySales: EventSales[];
  topEventsByAmount: EventSales[];
  status: RequestStatus;
  error: string | null;
}

export interface EventSales {
  eventId: string;
  eventName: string;
  salesCount: number;
  totalAmountInCents: number;
}