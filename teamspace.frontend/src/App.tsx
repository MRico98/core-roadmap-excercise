import React from 'react';
import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import NewUserPage from './components/pages/NewUserPage';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<div>Home</div>} />
        <Route path="/about" element={<div>About</div>} />
        <Route path="/register" element={<NewUserPage />} />
      </Routes>
    </BrowserRouter>
  )
}

export default App;
