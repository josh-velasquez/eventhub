import { fetchEvents } from "@/store/events/eventsSlice";
import { useAppDispatch, useAppSelector } from "@/store/hooks";
import type { EventsQuery } from "@/types/types";
import { useEffect, useState } from "react";

export const useFetchEvents = () => {
  const dispatch = useAppDispatch();
  const { events, status, error } = useAppSelector((state) => state.events);
  const [fetchEventsUnderway, setFetchEventsUnderway] =
    useState<boolean>(false);

  const fetchEventRows = async (eventsQuery: EventsQuery) => {
    await dispatch(fetchEvents(eventsQuery)).unwrap();
  };

  useEffect(() => {
    if (status === "pending") {
      setFetchEventsUnderway(true);
    } else {
      setFetchEventsUnderway(false);
    }
    if (error) {
      console.log("Failed to fetch events");
    }
  }, [status, error]);

  return { events, fetchEventsUnderway, fetchEventRows };
};
