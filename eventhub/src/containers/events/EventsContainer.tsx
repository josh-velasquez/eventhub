import { Sidebar } from "@/components/Sidebar";
import { useFetchSales } from "@/hooks/sales/useFetchSales";
import { useState } from "react";
import EventsDashboard from "./EventsDashboard";
import SalesSummary from "./SalesSummary";
import { useFetchEvents } from "@/hooks/events/useFetchEvents";



export default function EventsContainer() {
  const [activeSection, setActiveSection] = useState<string>("events");
  const { topEventsBySales, topEventsByAmount, fetchTopEventsBySalesUnderway, fetchTopEventsByAmountUnderway, fetchSalesSummaryTopEventsBySales, fetchSalesSummaryTopEventsByAmount } = useFetchSales();
  const { events, fetchEventsUnderway, fetchEventRows } = useFetchEvents();

  const sections = [
    {
      id: "events",
      label: "Events",
      render: () => <EventsDashboard
        events={events}
        fetchEventsUnderway={fetchEventsUnderway}
        fetchEventRows={fetchEventRows}
      />,
    },
    {
      id: "salessummary",
      label: "Sales Summary",
      render: () => (
        <SalesSummary
          topEventsBySales={topEventsBySales}
          topEventsByAmount={topEventsByAmount}
          fetchTopEventsBySalesUnderway={fetchTopEventsBySalesUnderway}
          fetchTopEventsByAmountUnderway={fetchTopEventsByAmountUnderway}
          fetchSalesSummaryTopEventsBySales={fetchSalesSummaryTopEventsBySales}
          fetchSalesSummaryTopEventsByAmount={fetchSalesSummaryTopEventsByAmount}
        />
      ),

    },
  ];

  const activeContent = sections.find((s) => s.id === activeSection)?.render();

  return (
    <div className="grid grid-cols-1 lg:grid-cols-6 gap-4 p-4 min-h-screen">
      <Sidebar title="Sections">
        <ul className="space-y-3">
          {sections.map((section) => (
            <li key={section.id}>
              <button
                className={`w-full text-left p-3 rounded-lg transition-all duration-200 ${activeSection === section.id
                  ? "bg-blue-500 text-white font-semibold shadow-lg"
                  : "hover:bg-blue-50 dark:hover:bg-gray-700 text-gray-700 dark:text-gray-300 hover:text-blue-600 dark:hover:text-blue-400"
                  }`}
                onClick={() => setActiveSection(section.id)}
              >
                {section.label}
              </button>
            </li>
          ))}
        </ul>
      </Sidebar>
      <div className="lg:col-span-5 space-y-4 w-full">{activeContent}</div>
    </div>
  );
}
