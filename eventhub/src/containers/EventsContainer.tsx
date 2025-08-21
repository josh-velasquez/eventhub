import { DataTable } from "@/components/DataTable";
import { Sidebar } from "@/components/Sidebar";
import { useFetchEvents } from "@/hooks/events/useFetchEvents";
import type { Event, Sorting } from "@/types/types";
import { useEffect, useState } from "react";

const mockEvents: Event[] = [
  {
    id: "1",
  name: "Tech Conference 2025",
    startsOn: "2025-09-10",
    endsOn: "2025-09-11",
    location: "New York",
  },
  {
    id: "2",
    name: "Startup Pitch Night",
    startsOn: "2025-09-12",
    endsOn: "2025-09-12",
    location: "San Francisco",
  },
  {
    id: "3",
    name: "React Meetup",
    startsOn: "2025-09-15",
    endsOn: "2025-09-15",
    location: "Toronto",
  },
  {
    id: "4",
    name: "AI Expo",
    startsOn: "2025-09-18",
    endsOn: "2025-09-19",
    location: "London",
  },
  {
    id: "5",
    name: "Cloud Summit",
    startsOn: "2025-09-22",
    endsOn: "2025-09-23",
    location: "Berlin",
  },
  {
    id: "6",
    name: "Blockchain Workshop",
    startsOn: "2025-09-25",
    endsOn: "2025-09-25",
    location: "Singapore",
  },
  {
    id: "7",
    name: "Hackathon 2025",
    startsOn: "2025-09-28",
    endsOn: "2025-09-29",
    location: "Sydney",
  },
  {
    id: "8",
    name: "UX Design Conference",
    startsOn: "2025-10-01",
    endsOn: "2025-10-02",
    location: "Paris",
  },
  {
    id: "9",
    name: "Cybersecurity Forum",
    startsOn: "2025-10-04",
    endsOn: "2025-10-05",
    location: "Dubai",
  },
  {
    id: "10",
    name: "DevOps Days",
    startsOn: "2025-10-07",
    endsOn: "2025-10-08",
    location: "Amsterdam",
  },

  {
    id: "11",
    name: "Mobile World Congress",
    startsOn: "2025-10-10",
    endsOn: "2025-10-12",
    location: "Barcelona",
  },
  {
    id: "12",
    name: "Green Energy Summit",
    startsOn: "2025-10-12",
    endsOn: "2025-10-13",
    location: "Stockholm",
  },
  {
    id: "13",
    name: "FinTech Expo",
    startsOn: "2025-10-15",
    endsOn: "2025-10-16",
    location: "London",
  },
  {
    id: "14",
    name: "AI & Robotics Workshop",
    startsOn: "2025-10-18",
    endsOn: "2025-10-18",
    location: "Seoul",
  },
  {
    id: "15",
    name: "E-commerce Summit",
    startsOn: "2025-10-20",
    endsOn: "2025-10-21",
    location: "New York",
  },
  {
    id: "16",
    name: "Data Science Meetup",
    startsOn: "2025-10-22",
    endsOn: "2025-10-22",
    location: "Toronto",
  },
  {
    id: "17",
    name: "VR & AR Conference",
    startsOn: "2025-10-25",
    endsOn: "2025-10-26",
    location: "San Francisco",
  },
  {
    id: "18",
    name: "Cloud Security Forum",
    startsOn: "2025-10-28",
    endsOn: "2025-10-29",
    location: "Berlin",
  },
  {
    id: "19",
    name: "IoT Expo",
    startsOn: "2025-11-01",
    endsOn: "2025-11-02",
    location: "Singapore",
  },
  {
    id: "20",
    name: "Digital Marketing Summit",
    startsOn: "2025-11-04",
    endsOn: "2025-11-05",
    location: "Paris",
  },

  {
    id: "21",
    name: "Blockchain Hackathon",
    startsOn: "2025-11-07",
    endsOn: "2025-11-09",
    location: "Sydney",
  },
  {
    id: "22",
    name: "Tech Startup Fair",
    startsOn: "2025-11-10",
    endsOn: "2025-11-11",
    location: "London",
  },
  {
    id: "23",
    name: "AI Ethics Workshop",
    startsOn: "2025-11-12",
    endsOn: "2025-11-12",
    location: "Amsterdam",
  },
  {
    id: "24",
    name: "Robotics Expo",
    startsOn: "2025-11-15",
    endsOn: "2025-11-16",
    location: "Tokyo",
  },
  {
    id: "25",
    name: "Software Dev Conference",
    startsOn: "2025-11-18",
    endsOn: "2025-11-19",
    location: "Toronto",
  },
  {
    id: "26",
    name: "Big Data Summit",
    startsOn: "2025-11-20",
    endsOn: "2025-11-21",
    location: "New York",
  },
  {
    id: "27",
    name: "UX/UI Design Meetup",
    startsOn: "2025-11-22",
    endsOn: "2025-11-22",
    location: "Berlin",
  },
  {
    id: "28",
    name: "FinTech Hackathon",
    startsOn: "2025-11-25",
    endsOn: "2025-11-26",
    location: "Singapore",
  },
  {
    id: "29",
    name: "AR/VR Gaming Expo",
    startsOn: "2025-11-28",
    endsOn: "2025-11-29",
    location: "San Francisco",
  },
  {
    id: "30",
    name: "Tech Leaders Forum",
    startsOn: "2025-12-01",
    endsOn: "2025-12-02",
    location: "Dubai",
  },
];

const DEFAULT_ROWS_PER_PAGE = 10;

const columns: { key: keyof Event; header: string }[] = [
  { key: "id", header: "ID" },
  { key: "name", header: "Name" },
  { key: "startsOn", header: "Start Date" },
  { key: "endsOn", header: "End Date" },
  { key: "location", header: "Location" },
];

export default function EventsContainer() {
  const [activeSection, setActiveSection] = useState<string>("events");
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [sortKey, setSortKey] = useState<keyof Event | null>(null);
  const [sortOrder, setSortOrder] = useState<Sorting>("asc");
  const [searchQuery, setSearchQuery] = useState<string>("");

  const { events, fetchEventsUnderway, fetchEventRows } = useFetchEvents();

  useEffect(() => {
    fetchEventRows({
      startDate: "",
      endDate: "",
      page: currentPage,
      pageSize: DEFAULT_ROWS_PER_PAGE,
      sortBy: sortKey?.toString() ?? "id",
      sortOrder,
      searchQuery,
    });
  }, [currentPage, sortKey, sortOrder, searchQuery]);

  const sections = [
    {
      id: "events",
      label: "Events",
      render: () => (
        <DataTable
          // data={events?.events ?? []}
          data={mockEvents}
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
      ),
    },
    {
      id: "salessummary",
      label: "Sales Summary",
      render: () => (
        <div className="bg-white dark:bg-gray-800 rounded-2xl shadow-lg p-6 transition-all">
          Summary
        </div>
      ),
    },
  ];

  const activeContent = sections.find((s) => s.id === activeSection)?.render();

  return (
    <div className="grid grid-cols-1 md:grid-cols-4 gap-6 p-6 min-h-screen">
      <Sidebar title="Sections">
        <ul className="space-y-2">
          {sections.map((section) => (
            <li key={section.id}>
              <button
                className={`w-full text-left p-2 rounded-lg ${
                  activeSection === section.id
                    ? "bg-blue-500 text-white font-semibold shadow"
                    : "hover:bg-blue-100 dark:hover:bg-gray-700 text-gray-800 dark:text-gray-200"
                }`}
                onClick={() => setActiveSection(section.id)}
              >
                {section.label}
              </button>
            </li>
          ))}
        </ul>
      </Sidebar>
      <div className="md:col-span-3 space-y-4">{activeContent}</div>
    </div>
  );
}
