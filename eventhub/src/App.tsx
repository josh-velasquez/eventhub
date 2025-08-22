import EventsContainer from "./containers/events/EventsContainer";

function App() {
  return (
    <div className="min-h-screen bg-gradient-to-br from-indigo-50 via-purple-50 dark:from-indigo-900 dark:via-purple-800 dark:to-pink-900 transition-colors p-6">
      <div className="max-w-7xl mx-auto">
        <h1 className="text-3xl font-bold mb-6 text-gray-900 dark:text-gray-100">
          Event Dashboard
        </h1>
        <EventsContainer />
      </div>
    </div>
  );
}

export default App;
