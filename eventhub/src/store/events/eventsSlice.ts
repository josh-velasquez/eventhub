import type { EventsResponse, EventsQuery, EventsState } from "@/types/types";
import {
  createAsyncThunk,
  createSlice,
  type PayloadAction,
} from "@reduxjs/toolkit";
import axios, { AxiosError } from "axios";

const initialState: EventsState = {
  events: null,
  status: "idle",
  error: null,
};

export const fetchEvents = createAsyncThunk<EventsResponse, EventsQuery>(
  "events/fetchEvents",
  async (eventsQuery, { rejectWithValue }) => {
    try {
      const response = await axios.get(
        `${import.meta.env.VITE_SERVER_ROOT_URL}/events`,
        { params: eventsQuery }
      );
      return response.data;
    } catch (err) {
      const error = err as AxiosError<{ message: string }>;
      return rejectWithValue({
        status: error.response?.status,
        message: error.response?.data.message || "Failed to fetch events",
      });
    }
  }
);

const eventsSlice = createSlice({
  name: "events",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchEvents.pending, (state) => {
        state.status = "pending";
        state.error = null;
      })
      .addCase(
        fetchEvents.fulfilled,
        (state, action: PayloadAction<EventsResponse>) => {
          state.status = "fulfilled";
          state.events = action.payload;
        }
      )
      .addCase(fetchEvents.rejected, (state, action) => {
        state.status = "failed";
        state.error = action.payload
          ? (action.payload as { message: string }).message
          : action.error.message || "Something went wrong";
      });
  },
});

export default eventsSlice.reducer;
