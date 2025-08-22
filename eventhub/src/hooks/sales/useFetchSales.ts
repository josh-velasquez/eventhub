import { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import type { AppDispatch, RootState } from '../../store/store';
import { fetchTopEventsBySales, fetchTopEventsByAmount } from '../../store/sales/salesSlice';

export const useFetchSales = () => {
  const dispatch = useDispatch<AppDispatch>();
  const [fetchTopEventsBySalesUnderway, setFetchTopEventsBySalesUnderway] = useState(false);
  const [fetchTopEventsByAmountUnderway, setFetchTopEventsByAmountUnderway] = useState(false);
  const { topEventsBySales, topEventsByAmount, status, error } = useSelector(
    (state: RootState) => state.sales
  );

  const fetchSalesSummaryTopEventsBySales = async (limit: number) => {
    await dispatch(fetchTopEventsBySales(limit)).unwrap();
  };

  const fetchSalesSummaryTopEventsByAmount = async (limit: number) => {
    await dispatch(fetchTopEventsByAmount(limit)).unwrap();
  }

  useEffect(() => {
    if (status === 'pending') {
        setFetchTopEventsBySalesUnderway(true);
        setFetchTopEventsByAmountUnderway(true);
    } else {
        setFetchTopEventsBySalesUnderway(false);
        setFetchTopEventsByAmountUnderway(false);
    }
    if (error) {
        console.log('Failed to fetch sales summary');
    }
  }, [status, error]);

  return {
    topEventsBySales,
    topEventsByAmount,
    fetchTopEventsBySalesUnderway,
    fetchTopEventsByAmountUnderway,
    fetchSalesSummaryTopEventsBySales,
    fetchSalesSummaryTopEventsByAmount
  };
};
