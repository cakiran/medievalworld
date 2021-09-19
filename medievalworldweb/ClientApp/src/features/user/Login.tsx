import React, { Fragment, useState } from "react";
//External libraries
import { useForm } from "react-hook-form";
import { Redirect } from "react-router-dom";
import { UserDetailsApi } from "../../api/agent";
import { IUserDetail,FormData } from "../../model/interfaces";
import { toast } from "react-toastify";
import { Button, Divider, Grid, Segment } from "semantic-ui-react";


const Login: React.FC = () => {
  const [navigateToFight, setNavigateToFight] = useState(false);
  const [navigateToRegister, setNavigateToRegister] = useState(false);
  const { register, handleSubmit, errors } = useForm<FormData>();
  const onSubmit = handleSubmit(({ username, password }) => {
    const userDetail: IUserDetail = { username: username, password: password };
    UserDetailsApi.login(userDetail)
      .then((response) => {
        if (response?.success === true) {
          window.localStorage.setItem("jwt", response.data);
          setNavigateToFight(true);
        } else {
          toast(response.message);
        }
      })
      .finally(() => {});
  });

  const goToRegister = () => {
    setNavigateToRegister(true);
  };
  if (navigateToFight) return <Redirect to="/FightArena" push />;
  if (navigateToRegister) return <Redirect to="/Register" push />;
  return (
    <Fragment>
      <Segment placeholder>
        <Grid columns={2} relaxed="very" stackable>
          <Grid.Column>
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
              <input type="submit" value="Login" />
            </form>
          </Grid.Column>

          <Grid.Column verticalAlign="middle">
            <Button
              content="Sign up"
              icon="signup"
              size="big"
              onClick={goToRegister}
            />
          </Grid.Column>
        </Grid>

        <Divider vertical>Or</Divider>
      </Segment>
    </Fragment>
  );
};
export default Login;
