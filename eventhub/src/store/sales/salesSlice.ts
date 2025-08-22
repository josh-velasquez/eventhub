import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import type { PayloadAction } from '@reduxjs/toolkit';
import type { EventSales, SalesState } from '@/types/types';
import axios from 'axios';

const initialState: SalesState = {
  topEventsBySales: [],
  topEventsByAmount: [],
  status: 'idle',
  error: null,
};

export const fetchTopEventsBySales = createAsyncThunk<EventSales[], number>(
  'sales/fetchTopEventsBySales',
  async (limit: number = 5) => {
    try {
      const response = await axios.get(`${import.meta.env.VITE_SERVER_ROOT_URL}/sales/analytics/sales-count`, {
        params: {
          limit
        }
      });
      return response.data;
    } catch (error) {
      throw new Error('Failed to fetch top events by sales');
    } 
  }
);

export const fetchTopEventsByAmount = createAsyncThunk<EventSales[], number>(
  'sales/fetchTopEventsByAmount',
  async (limit: number = 5) => {
    try {
      const response = await axios.get(`${import.meta.env.VITE_SERVER_ROOT_URL}/sales/analytics/total-amount`, {
        params: {
          limit
        }
      });
      return response.data;
    } catch (error) {
      throw new Error('Failed to fetch top events by amount');
    }
  }
);

const salesSlice = createSlice({
  name: 'sales',
  initialState,
  reducers: {
    clearError: (state) => {
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchTopEventsBySales.pending, (state) => {
        state.status = 'pending';
        state.error = null;
      })
      .addCase(fetchTopEventsBySales.fulfilled, (state, action: PayloadAction<EventSales[]>) => {
        state.status = 'fulfilled';
        state.topEventsBySales = action.payload;
      })
      .addCase(fetchTopEventsBySales.rejected, (state, action) => {
        state.status = 'failed';
        state.error = action.error.message || 'Failed to fetch top events by sales';
      })
      .addCase(fetchTopEventsByAmount.pending, (state) => {
        state.status = 'pending';
        state.error = null;
      })
      .addCase(fetchTopEventsByAmount.fulfilled, (state, action: PayloadAction<EventSales[]>) => {
        state.status = 'fulfilled';
        state.topEventsByAmount = action.payload;
      })
      .addCase(fetchTopEventsByAmount.rejected, (state, action) => {
        state.status = 'failed';
        state.error = action.error.message || 'Failed to fetch top events by amount';
      });
  },
});

export const { clearError } = salesSlice.actions;
export default salesSlice.reducer;
