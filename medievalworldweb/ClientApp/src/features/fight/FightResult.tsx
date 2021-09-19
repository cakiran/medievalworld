import React from "react";
import { Fragment } from "react";
import { List, Image, Grid } from "semantic-ui-react";
import { IFightDetailsProps } from "../../model/interfaces";

const FightResult: React.FC<IFightDetailsProps> = ({ fightDetails }) => {
  return (
    <Fragment>
        <Grid centered columns={1}>
            <Grid.Column>
            <h5 className="card-title">Fight Result</h5>
            </Grid.Column>
     
              <List>
                {fightDetails.map((item: any, index) => {
                  return <List.Item key={index}>{item.logText}</List.Item>;
                })}
                {fightDetails.length < 1 ? (
                  <List.Item>
                    <Image
                      centered
                      src="/fightbegin.gif"
                      alt=""
                      circular
                      height={75}
                      width={200}
                    />
                  </List.Item>
                ) : fightDetails.some((fa) => fa.winnerId) ? (
                  <List.Item>
                    <Image
                      centered
                      src="/peace.jpeg"
                      alt=""
                      circular
                      height={75}
                      width={200}
                    />
                  </List.Item>
                ) : (
                  <List.Item>
                    <Image
                      centered
                      src="/SwordFight.gif"
                      alt=""
                      circular
                      height={75}
                      width={200}
                    />
                  </List.Item>
                )}
              </List>
              </Grid>
    </Fragment>
  );
};
export default FightResult;
