export function Spinner({ className = "" }: { className?: string }) {
  return (
    <div
      className={`animate-spin rounded-full border-4 border-t-4 border-gray-200 dark:border-gray-700 border-t-blue-500 h-8 w-8 ${className}`}
    />
  );
}
