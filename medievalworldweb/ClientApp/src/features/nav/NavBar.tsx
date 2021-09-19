import React, { useState, useEffect } from "react";
//External libraries
import { Menu } from "semantic-ui-react";
import { Redirect } from "react-router-dom";
import jwt_decode from "jwt-decode";
//Interfaces
import { INavBarProps } from "../../model/interfaces";

const NavBar: React.FC<INavBarProps> = ({ setUserId }) => {
  const [userName, setUserName] = useState("");
  const [navigateToLogin, setNavigateToLogin] = useState(false);

  useEffect(() => {
    const token = window.localStorage.getItem("jwt");
    if (token) {
      const decoded: any = jwt_decode(token);
      if (decoded.unique_name) setUserName(decoded.unique_name);
      if (decoded.nameid) setUserId(decoded.nameid);
    }
  }, [setUserId])

  const goToLogin = () => {
    if (window.confirm("Are you sure you want to logoff?")) {
      window.localStorage.setItem("jwt", "");
      setNavigateToLogin(true);
    }
  }

  if (navigateToLogin) return <Redirect to="/" push />;
  return (
    <div>
      <Menu stackable>
        <Menu.Item>
          <img src="/dagger.jpg" alt="dagger"/>
        </Menu.Item>

        <Menu.Item name="features" header>Medieval World</Menu.Item>
        <Menu.Menu position="right">
          <Menu.Item name="features">{`Welcome ${userName}`}</Menu.Item>
          <Menu.Item name="logout" onClick={goToLogin} />
        </Menu.Menu>
      </Menu>
    </div>
  );
}

export default NavBar;
