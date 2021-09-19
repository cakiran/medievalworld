import "./App.css";
import { Route } from "react-router-dom";

import React, { useState } from "react";
import NavBar from "./features/nav/NavBar";
import Login from "./features/user/Login";
import Register from "./features/user/Register";
import FightArena from "./features/fight/FightArena";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.min.css";
import { Container } from "semantic-ui-react";
import Footer from "./features/nav/Footer";

const App: React.FC = () => {
  const [userId, saveUserId] = useState(0);
  const setUserId = (userId: number) => {
    saveUserId(userId);
  };
  return (
    <Container>
      <NavBar setUserId={setUserId} />
      <Route path="/" exact render={(props) => <Login />} />
      <Route path="/Login" exact component={Login} />
      <Route path="/Register" exact component={Register} />
      <Route
        path="/FightArena"
        exact
        render={(props) => <FightArena userId={userId} />}
      />
      <br/>
      <Footer/>
      <ToastContainer position="bottom-right" />
    </Container>
  );
};
export default App;
