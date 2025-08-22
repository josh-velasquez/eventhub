import { Card, CardContent, CardHeader, CardTitle } from "./ui/card";

interface SidebarProps {
  title: string;
  children: React.ReactNode;
}

export function Sidebar({ title, children }: SidebarProps) {
  return (
    <Card className="rounded-2xl shadow-lg p-4 h-fit">
      <CardHeader className="pb-1">
        <CardTitle className="text-md font-semibold text-gray-800 dark:text-gray-200">
          {title}
        </CardTitle>
        <div className="w-12 h-1 bg-gradient-to-r from-blue-500 to-purple-500 rounded-full mt-2"></div>
      </CardHeader>
      <CardContent className="pt-2">{children}</CardContent>
    </Card>
  );
}
