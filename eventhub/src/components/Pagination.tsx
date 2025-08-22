import type { JSX } from "react";
import { Button } from "./ui/button";

interface PaginationProps {
  currentPage: number;
  totalPages: number;
  onPageChange: (page: number) => void;
}

export function Pagination({
  currentPage,
  totalPages,
  onPageChange,
}: PaginationProps): JSX.Element {
  const isFirstPage = currentPage === 1;
  const isLastPage = currentPage === totalPages;
  return (
    <div className="flex justify-between items-center mt-4 w-full max-w-md mx-auto px-4">
      <Button
        variant="outline"
        disabled={isFirstPage}
        onClick={() => onPageChange(currentPage - 1)}
      >
        Previous
      </Button>
      <span className="text-sm">
        Page {currentPage} of {totalPages}
      </span>
      <Button
        variant="outline"
        disabled={isLastPage}
        onClick={() => onPageChange(currentPage + 1)}
      >
        Next
      </Button>
    </div>
  );
}
