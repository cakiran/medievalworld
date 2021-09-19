import React from "react";
//External libraries
import { Menu } from "semantic-ui-react";

const Footer = () => {
  
  return (
    <div>
      <Menu borderless fluid widths={2}>
        <Menu.Item>
          <img src="/dagger.jpg" alt="dagger" />
        </Menu.Item>
        <Menu.Item name="features">© 2021 Medieval World</Menu.Item>
      </Menu>
    </div>
  );
}

export default Footer;
