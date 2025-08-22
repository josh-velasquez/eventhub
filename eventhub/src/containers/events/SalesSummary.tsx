

import { useEffect } from 'react';
import { Card, CardContent, CardHeader, CardTitle } from '../../components/ui/card';
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from '../../components/ui/table';
import { Spinner } from '../../components/ui/spinner';
import type { EventSales } from '../../types/types';

interface SalesSummaryContainerProps {
  topEventsBySales: EventSales[];
  topEventsByAmount: EventSales[];
  fetchTopEventsBySalesUnderway: boolean;
  fetchTopEventsByAmountUnderway: boolean;
  fetchSalesSummaryTopEventsBySales: (limit: number) => Promise<void>;
  fetchSalesSummaryTopEventsByAmount: (limit: number) => Promise<void>;
}

export default function SalesSummary({
  topEventsBySales,
  topEventsByAmount,
  fetchTopEventsBySalesUnderway,
  fetchTopEventsByAmountUnderway,
  fetchSalesSummaryTopEventsBySales,
  fetchSalesSummaryTopEventsByAmount
}: SalesSummaryContainerProps) {

  useEffect(() => {

    fetchSalesSummaryTopEventsBySales(5);
    fetchSalesSummaryTopEventsByAmount(5);
  }, []);

  const isLoading = fetchTopEventsBySalesUnderway || fetchTopEventsByAmountUnderway;

  if (isLoading) {
    return (
      <div className="flex justify-center items-center h-64">
        <Spinner />
      </div>
    );
  }



  return (
    <div className="space-y-6">
      <h1 className="text-2xl font-bold">Sales Summary</h1>

      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
        <Card>
          <CardHeader>
            <CardTitle>Top 5 Events by Sales Count</CardTitle>
          </CardHeader>
          <CardContent>
            <Table>
              <TableHeader>
                <TableRow>
                  <TableHead>Event Name</TableHead>
                  <TableHead>Sales Count</TableHead>
                  <TableHead>Total Revenue</TableHead>
                </TableRow>
              </TableHeader>
              <TableBody>
                {topEventsBySales.map((event) => (
                  <TableRow key={event.eventId}>
                    <TableCell className="font-medium">{event.eventName}</TableCell>
                    <TableCell>{event.salesCount}</TableCell>
                    <TableCell>${(event.totalAmountInCents / 100).toFixed(2)}</TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </CardContent>
        </Card>

        <Card>
          <CardHeader>
            <CardTitle>Top 5 Events by Revenue</CardTitle>
          </CardHeader>
          <CardContent>
            <Table>
              <TableHeader>
                <TableRow>
                  <TableHead>Event Name</TableHead>
                  <TableHead>Sales Count</TableHead>
                  <TableHead>Total Revenue</TableHead>
                </TableRow>
              </TableHeader>
              <TableBody>
                {topEventsByAmount.map((event) => (
                  <TableRow key={event.eventId}>
                    <TableCell className="font-medium">{event.eventName}</TableCell>
                    <TableCell>{event.salesCount}</TableCell>
                    <TableCell>${(event.totalAmountInCents / 100).toFixed(2)}</TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </CardContent>
        </Card>
      </div>
    </div>
  );
}