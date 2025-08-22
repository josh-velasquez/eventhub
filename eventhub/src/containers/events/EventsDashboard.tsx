import { DataTable } from "@/components/DataTable";
import type { Event, EventsQuery, EventsResponse, Sorting } from "@/types/types";
import { useEffect, useState } from "react";

const DEFAULT_ROWS_PER_PAGE = 10;

const columns: { key: keyof Event; header: string }[] = [
  { key: "name", header: "Name" },
  { key: "startsOn", header: "Start Date" },
  { key: "endsOn", header: "End Date" },
  { key: "location", header: "Location" },
];

interface EventsDashboardContainerProps {
  events: EventsResponse | null;
  fetchEventsUnderway: boolean; 
  fetchEventRows: (query: EventsQuery) => void;
}

export default function EventsDashboard({ events, fetchEventsUnderway, fetchEventRows }: EventsDashboardContainerProps) {
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [sortKey, setSortKey] = useState<keyof Event | null>(null);
  const [sortOrder, setSortOrder] = useState<Sorting>("asc");
  const [searchQuery, setSearchQuery] = useState<string>("");



  useEffect(() => {
    fetchEventRows({
      startDate: "",
      endDate: "",
      page: currentPage,
      pageSize: DEFAULT_ROWS_PER_PAGE,
      sortBy: sortKey?.toString() ?? "name",
      sortOrder,
      searchQuery,
    });
  }, [currentPage, sortKey, sortOrder, searchQuery]);

  return (
    <div className="space-y-6">
      <div className="flex justify-between items-center">
        <h1 className="text-2xl font-bold">Events Dashboard</h1>
        <div className="text-sm text-gray-600">
          Showing {events?.events?.length || 0} of {events?.totalCount || 0} events
        </div>
      </div>
      
      <DataTable
        data={events?.events ?? []}
        columns={columns}
        currentPage={events?.currentPage ?? 1}
        totalPages={events?.totalPages ?? 1}
        onPageChange={setCurrentPage}
        onSortChange={(key, order) => {
          setSortKey(key);
          setSortOrder(order);
          setCurrentPage(1);
        }}
        sortKey={sortKey}
        sortOrder={sortOrder}
        onSearch={(query) => {
          setSearchQuery(query);
          setCurrentPage(1);
        }}
        loading={fetchEventsUnderway}
      />
    </div>
  );
}
