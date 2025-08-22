import { configureStore } from "@reduxjs/toolkit";
import eventsReducer from "./events/eventsSlice";
import salesReducer from "./sales/salesSlice";

const store = configureStore({
  reducer: {
    events: eventsReducer,
    sales: salesReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export default store;
