import React, { Fragment, useState } from "react";
//css
import "./styles.css";
//External libraries
import { Redirect } from "react-router-dom";
import { toast } from "react-toastify";
import { useForm } from "react-hook-form";
//Interfaces and internal references
import { UserDetailsApi } from "../../api/agent";
import { IUserDetail,RegisterFormData } from "../../model/interfaces";

const Register: React.FC = () => {
  const [navigateToRegister] = useState(false);
  const [navigateToLogin, setNavigateToLogin] = useState(false);
  const { register, handleSubmit, errors } = useForm<RegisterFormData>();
  const onSubmit = handleSubmit(({ username, password, password1 }) => {
    if (password.trim() !== password1.trim())
      toast("Passwords don't match. Please use matching passwords.");
    const userDetail: IUserDetail = { username: username, password: password1 };
    UserDetailsApi.register(userDetail)
      .then((response) => {
        console.log("response", response);
        if (response?.success === true) setNavigateToLogin(true);
        else {
          toast(response.message);
        }
      })
      .finally(() => {});
  });

  if (navigateToLogin) return <Redirect to="/Login" push />;

  if (navigateToRegister) return <Redirect to="/Register" push />;
  return (
    <Fragment>
      <form onSubmit={onSubmit}>
        <label>User Name</label>
        <input
          name="username"
          ref={register({ required: true, maxLength: 20 })}
        />
        {errors.username && "Username is required"}
        <label>Password</label>
        <input
          name="password"
          ref={register({ required: true, maxLength: 20 })}
          type="password"
        />
        {errors.password && "Password is required"}
        <label>Retype Password</label>
        <input
          name="password1"
          ref={register({ required: true, maxLength: 20 })}
          type="password"
        />
        {errors.password1 && "Password is required"}
        <input type="submit" value="Register" />
      </form>
    </Fragment>
  );
};
export default Register;
