-- Database optimization indexes for better query performance

-- Index on EventId for faster ticket lookups by event
CREATE INDEX IF NOT EXISTS idx_tickets_eventid ON TicketSales(EventId);

-- Index on UserId for faster ticket lookups by user
CREATE INDEX IF NOT EXISTS idx_tickets_userid ON TicketSales(UserId);

-- Index on PurchaseDate for faster date range queries
CREATE INDEX IF NOT EXISTS idx_tickets_purchasedate ON TicketSales(PurchaseDate);

-- Composite index for EventId + PurchaseDate for event-specific date queries
CREATE INDEX IF NOT EXISTS idx_tickets_eventid_purchasedate ON TicketSales(EventId, PurchaseDate);

-- Index on PriceInCents for sorting and aggregation
CREATE INDEX IF NOT EXISTS idx_tickets_priceincents ON TicketSales(PriceInCents);

-- Index on Event Id in Events table for joins
CREATE INDEX IF NOT EXISTS idx_events_id ON Events(Id);

-- Composite index for EventId + PriceInCents for aggregation queries
CREATE INDEX IF NOT EXISTS idx_tickets_eventid_priceincents ON TicketSales(EventId, PriceInCents);
