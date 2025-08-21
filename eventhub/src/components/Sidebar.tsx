import { Card, CardContent, CardHeader, CardTitle } from "./ui/card";

interface SidebarProps {
  title: string;
  children: React.ReactNode;
}

export function Sidebar({ title, children }: SidebarProps) {
  return (
    <Card className="rounded-2xl shadow p-4">
      <CardHeader>
        <CardTitle className="text-md font-semibold" />
        {title}
      </CardHeader>
      <CardContent>{children}</CardContent>
    </Card>
  );
}
