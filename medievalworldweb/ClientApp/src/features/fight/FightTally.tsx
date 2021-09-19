import React from "react";
import { Fragment } from "react";
import { Label, Table } from "semantic-ui-react";
import { IFightTallyProps } from "../../model/interfaces";

const FightTally: React.FC<IFightTallyProps> = ({ fightTally }) => {
  return (
    <Fragment>
      <h5 className="card-title">Fight Tally</h5>
      <Table color={"green"} >
        <Table.Header>
          <Table.Row>
            <Table.HeaderCell>Name</Table.HeaderCell>
            <Table.HeaderCell>Fights</Table.HeaderCell>
            <Table.HeaderCell>Victories</Table.HeaderCell>
            <Table.HeaderCell>Defeats</Table.HeaderCell>
          </Table.Row>
        </Table.Header>
        <Table.Body>
          {fightTally.map((item: any, index) => {
            return (
              <Table.Row key={index}>
                <Table.Cell>
                <Label ribbon>{item.username}</Label>
                </Table.Cell>
                <Table.Cell>{item.fights}</Table.Cell>
                <Table.Cell>{item.victories}</Table.Cell>
                <Table.Cell>{item.defeats}</Table.Cell>
              </Table.Row>
            );
          })}
        </Table.Body>
      </Table>
    </Fragment>
  );
};
export default FightTally;
