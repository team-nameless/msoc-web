import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter, Routes, Route } from "react-router-dom";

import "./index.css";

import App from "./App";
import Home from "./routes/Home";
import Tournament from "./routes/Tournament";
import About from "./routes/About";
import Leaderboard from "./routes/Leaderboard";
import Info from "./routes/Info";
import NotFound from "./routes/NotFound";
import BanPickContent from "./component/BanPick/BanPickContent";
import FinalContent from "./component/BanPick/FinalContent";
import BanPick from "./routes/BanPick";

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <React.StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<App />}>
          <Route path="/" element={<Home />}></Route>
          <Route path="/home" element={<Home />}></Route>
          <Route path="/tournament" element={<Tournament />}></Route>
          <Route path="/info" element={<Info />}></Route>
          <Route path="/leaderboard" element={<Leaderboard />}></Route>
          <Route path="/about" element={<About />}></Route>

          <Route path="/banpick" element={<BanPick />}>
            <Route path="/banpick/" element={<BanPickContent />}></Route>
            <Route path="/banpick/final" element={<FinalContent />}></Route>
          </Route>

          <Route path="*" element={<NotFound />}></Route>
        </Route>
      </Routes>
    </BrowserRouter>
  </React.StrictMode>
);
