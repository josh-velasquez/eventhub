import { Pagination } from "./Pagination";
import { Card, CardContent } from "./ui/card";
import { cn, formatDateTime } from "@/lib/utils";
import { ArrowDown, ArrowUp, ArrowUpDown } from "lucide-react";
import type { Sorting } from "@/types/types";
import { Spinner } from "./ui/spinner";


interface DataTableProps<T> {
  data: T[];
  columns: { key: keyof T; header: string }[];
  currentPage: number;
  totalPages: number;
  onPageChange: (page: number) => void;
  onSortChange: (key: keyof T, order: Sorting) => void;
  sortKey: keyof T | null;
  sortOrder: Sorting;
  onSearch: (query: string) => void;
  loading: boolean;
}

export function DataTable<T extends { id: string | number }>({
  data,
  columns,
  currentPage,
  totalPages,
  onPageChange,
  onSortChange,
  sortKey,
  sortOrder,
  onSearch,
  loading,
}: DataTableProps<T>) {
  return (
    <Card className="relative rounded-2xl shadow-lg p-6 bg-white dark:bg-gray-800 w-full">
      {loading && (
        <div className="absolute inset-0 flex items-center justify-center bg-white/70 dark:bg-gray-800/70 z-10">
          <Spinner className="w-8 h-8 text-blue-500" />
        </div>
      )}
      <div className="mb-4">
        <input
          type="text"
          placeholder="Search..."
          className="border border-gray-300 dark:border-gray-600 rounded-lg p-2 w-full focus:outline-none focus:ring-2 focus:ring-blue-500 dark:bg-gray-700 dark:text-white"
          onChange={(e) => {
            onSearch(e.target.value);
          }}
        />
      </div>
      <CardContent>
        <table className="w-full text-left border-collapse">
          <thead>
            <tr className="bg-indigo-50 dark:bg-purple-800">
              {columns.map((col) => (
                <th
                  key={String(col.key)}
                  onClick={() =>
                    onSortChange(
                      col.key,
                      sortKey === col.key && sortOrder === "asc"
                        ? "desc"
                        : "asc"
                    )
                  }
                  className={cn(
                    "border-b border-gray-200 dark:border-gray-600 p-3 text-sm font-semibold cursor-pointer select-none",
                    sortKey === col.key && "text-primary"
                  )}
                >
                  <div className="flex items-center justify-between">
                    {col.header}
                    {sortKey === col.key ? (
                      <span>
                        {sortOrder === "asc" ? (
                          <ArrowUp className="w-4 h-4 text-blue-600 dark:text-blue-400" />
                        ) : (
                          <ArrowDown className="w-4 h-4 text-blue-600 dark:text-blue-400" />
                        )}
                      </span>
                    ) : (
                      <ArrowUpDown className="w-4 h-4 text-gray-400 dark:text-gray-400" />
                    )}
                  </div>
                </th>
              ))}
            </tr>
          </thead>
          <tbody>
            {data.length > 0 ? (
              data.map((row, idx) => (
                <tr
                  key={row.id}
                  className={cn(
                    idx % 2 === 0
                      ? "bg-gray-50 dark:bg-gray-700"
                      : "bg-white dark:bg-gray-800",
                    "hover:bg-indigo-100 dark:hover:bg-purple-700 transition-colors"
                  )}
                >
                  {columns.map((col) => (
                    <td
                      key={String(col.key)}
                      className="p-3 text-sm text-gray-800 dark:text-gray-200"
                    >
                      {formatDateTime(row[col.key], String(col.key))}
                    </td>
                  ))}
                </tr>
              ))
            ) : (
              <tr>
                <td
                  colSpan={columns.length}
                  className="p-4 text-center text-sm text-gray-500 dark:text-gray-400"
                >
                  No data found
                </td>
              </tr>
            )}
          </tbody>
        </table>
        <div className="mt-6 flex justify-center">
          <Pagination
            currentPage={currentPage}
            totalPages={totalPages}
            onPageChange={onPageChange}
          />
        </div>
      </CardContent>
    </Card>
  );
}
