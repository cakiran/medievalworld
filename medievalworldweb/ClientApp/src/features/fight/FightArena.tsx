import React from "react";
//External libraries
import {
  DragDropContext,
  Draggable,
  Droppable,
  DroppableProvided,
  DraggableLocation,
  DropResult,
  DroppableStateSnapshot,
  DraggableProvided,
  DraggableStateSnapshot,
} from "react-beautiful-dnd";
import {
  Progress,
  Grid,
  Image,
  Header,
  Button,
} from "semantic-ui-react";
//Interfaces and internal references
import {
  IFightRequestDto,
  IFightArenaProps,
  IAddSelectedFightersDto,
  ISelectedFighter,
  Item,
  IMoveResult,
  IAppState,
} from "../../model/interfaces";
import gandalf from "../../images/gandalf.jpg";
import frodo from "../../images/frodo.jpg";
import Aragorn from "../../images/Aragorn.png";
import Legolas from "../../images/Legolas.jpg";
import { FightDetailsApi } from "../../api/agent";
import FightTally from "./FightTally";
import FightResult from "./FightResult";
import { toast } from "react-toastify";

const reorder = (
  list: Item[],
  startIndex: number,
  endIndex: number
): Item[] => {
  const result = [...list];
  const [removed] = result.splice(startIndex, 1);
  result.splice(endIndex, 0, removed);
  return result;
}
/**
 * Moves an item from one list to another list.
 */
const move = (
  source: Item[],
  destination: Item[],
  droppableSource: DraggableLocation,
  droppableDestination: DraggableLocation
): IMoveResult | any => {
  const sourceClone = [...source];
  const destClone = [...destination];
  const [removed] = sourceClone.splice(droppableSource.index, 1);
  destClone.splice(droppableDestination.index, 0, removed);
  const result = {};
  result[droppableSource.droppableId] = sourceClone;
  result[droppableDestination.droppableId] = destClone;
  return result;
}

const grid: number = 8;

const getItemStyle = (
  draggableStyle: any,
  isDragging: boolean,
  isWinner: boolean
): {} => ({
  userSelect: "none",
  padding: 2 * grid,
  margin: `0 0 ${grid}px 0`,
  background: isDragging || isWinner ? "lightgreen" : "grey",
  ...draggableStyle,
})

const getListStyle = (isDraggingOver: boolean): {} => ({
  background: isDraggingOver ? "lightblue" : "lightgrey",
  padding: grid,
  width: 250,
  minHeight: 400,
})

export default class FightArena extends React.Component<IFightArenaProps,IAppState> {
  public id2List = {
    droppable: "items",
    droppable2: "selected",
  };

  public timer: any = 0;
  constructor(props: IFightArenaProps) {
    super(props);
    this.state = {
      items: [],
      selected: [],
      fightTally: [],
      fightResult: [],
      fightAttackDetails: [],
      opponentId: -1,
      opponentHP: 100,
      winnerId: -1,
      attackerHP: 100,
      userId: 0,
      isUnlocked:false,
      btnFightDisabled:false
    }
  }

   getList = (id: string): Item[] =>  {
    return this.state[this.id2List[id]];
  }

   onDragEnd = (result: DropResult): void => {
    const { source, destination } = result;
    if (!destination) {
      return;
    }
    if (source.droppableId === destination.droppableId) {
      const items = reorder(
        this.getList(source.droppableId),
        source.index,
        destination.index
      );
      let state: IAppState = { ...this.state };
      if (source.droppableId === "droppable2") {
        state = { ...this.state, selected: items };
      } else if (source.droppableId === "droppable") {
        state = { ...this.state, items };
      }
      this.setState(state);
    } else {
      const resultFromMove: IMoveResult = move(
        this.getList(source.droppableId),
        this.getList(destination.droppableId),
        source,
        destination
      );
      this.setState({
        items: resultFromMove.droppable,
        selected: resultFromMove.droppable2,
      });

      if (resultFromMove.droppable2.length > 1) {
        this.setState({
          isUnlocked: true
        });
      } else {
        this.setState({
          isUnlocked: false
        });
      }
  
      const addSelectedfightersDto: IAddSelectedFightersDto = {
        selectedFighters: [],
      };
      resultFromMove.droppable2.forEach((fighter) => {
        let selectedFighter: ISelectedFighter = {
          id: parseInt(fighter.id),
          userId: fighter.userId,
          content: fighter.content,
        };
        addSelectedfightersDto.selectedFighters.push(selectedFighter);
      });
    }
  }
  
  updateFightAttackDetails = async () => {
    const fightDetails = await FightDetailsApi.getFightDetail();
    const fightTally = await FightDetailsApi.getFightTally(this.props.userId);
    let lastItemInFightDetailsArray: any = null;
    if (fightDetails.data && fightDetails.data.length > 0) {
      lastItemInFightDetailsArray =
        fightDetails.data[fightDetails.data.length - 1];
    }
    this.setState({
      fightAttackDetails: fightDetails.data,
      opponentId: lastItemInFightDetailsArray?.opponentId,
      opponentHP: lastItemInFightDetailsArray?.opponentHP,
      winnerId: lastItemInFightDetailsArray?.winnerId,
      attackerHP: lastItemInFightDetailsArray?.attackerHP,
      fightTally: fightTally.data
    });
  }

  handleFight = async () => {
    if(this.state.selected.length < 2)
    {
    toast("Please drag and drop 2 fighers to start the fight.");
    return;
    }
    this.setState({btnFightDisabled:true});
    let selectedFighters: number[] = [];
    this.state.selected.forEach((fighter: Item) => {
      selectedFighters.push(parseInt(fighter.id));
    });
    const fightRequestDto: IFightRequestDto = { fighters: selectedFighters };
    const fightRes = await FightDetailsApi.createFight(fightRequestDto);
    const fightTally = await FightDetailsApi.getFightTally(this.props.userId);
    this.setState({
      fightResult: fightRes.data.fightLog,
      fightTally: fightTally.data,
    });
  }

  handleReset = async () => {
    this.setState({btnFightDisabled:false});
    const fightTally = await FightDetailsApi.getFightTally(this.props.userId);
    const fightersFromApi = await FightDetailsApi.getFighters();
    fightersFromApi.data.forEach((f) => {
      f.id = f.id.toString();
      f.content = f.name;
      if (f.name.toLowerCase() === "gandalf") f.image = gandalf;
      if (f.name.toLowerCase() === "legolas") f.image = Legolas;
      if (f.name.toLowerCase() === "aragorn") f.image = Aragorn;
      if (f.name.toLowerCase() === "frodo") f.image = frodo;
    });
    this.setState({
      fightTally: fightTally.data,
      items: fightersFromApi.data,
      selected: [],
      isUnlocked:false
    });
    FightDetailsApi.reset(this.state.userId);
  }

  componentDidMount = async () => {
    const fightTally = await FightDetailsApi.getFightTally(this.props.userId);
    const fightersFromApi = await FightDetailsApi.getFighters();
    fightersFromApi.data.forEach((fighter) => {
      fighter.id = fighter.id.toString();
      fighter.content = fighter.name;
      if (fighter.name.toLowerCase() === "gandalf") fighter.image = gandalf;
      if (fighter.name.toLowerCase() === "legolas") fighter.image = Legolas;
      if (fighter.name.toLowerCase() === "aragorn") fighter.image = Aragorn;
      if (fighter.name.toLowerCase() === "frodo") fighter.image = frodo;
    });
    this.setState({ fightTally: fightTally.data, items: fightersFromApi.data });
    this.timer = setInterval(() => {
      this.updateFightAttackDetails();
    }, 1000);
  }

  componentWillUnMount = () => {
    clearInterval(this.timer);
    this.timer = null;
  }

  render() {
    return (
      <div>
        <Grid celled>
          <Grid.Row>
          <Grid.Column width={16}>
              <FightTally fightTally={this.state.fightTally} />
            </Grid.Column>
          </Grid.Row>
          <Grid.Row>
            <DragDropContext onDragEnd={this.onDragEnd}>
              <Grid.Column width={4}>
                <h5 className="card-title">Available Fighters</h5>
                <Droppable droppableId="droppable">
                  {(
                    provided: DroppableProvided,
                    snapshot: DroppableStateSnapshot
                  ) => (
                    <div
                      ref={provided.innerRef}
                      {...provided.droppableProps}
                      style={getListStyle(snapshot.isDraggingOver)}
                    >
                      {this.state.items.map((item, index) => (
                        <Draggable
                          key={item.id}
                          draggableId={item.id}
                          index={index}>
                          {(
                            providedDraggable: DraggableProvided,
                            snapshotDraggable: DraggableStateSnapshot
                          ) => (
                            <div>
                              <div
                                ref={providedDraggable.innerRef}
                                {...providedDraggable.draggableProps}
                                {...providedDraggable.dragHandleProps}
                                style={getItemStyle(
                                  providedDraggable.draggableProps.style,
                                  snapshotDraggable.isDragging,
                                  false
                                )}>
                                <Header as="h2">
                                  <Image circular src={item.image} />{" "}
                                  {item.content}
                                </Header>
                              </div>
                              {providedDraggable.innerRef}
                            </div>
                          )}
                        </Draggable>
                      ))}
                      {provided.placeholder}
                    </div>
                  )}
                </Droppable>
              </Grid.Column>
              <Grid.Column width={4}>
                <h5 className="card-title">Chosen Fighters</h5>
                <Droppable droppableId="droppable2" isDropDisabled={this.state.isUnlocked}>
                  {(
                    providedDroppable2: DroppableProvided,
                    snapshotDroppable2: DroppableStateSnapshot
                  ) => (
                    <div
                      ref={providedDroppable2.innerRef}
                      style={getListStyle(snapshotDroppable2.isDraggingOver)}
                    >
                      {this.state.selected.map((item, index) => (
                        <Draggable
                          key={item.id}
                          draggableId={item.id}
                          index={index}
                        >
                          {(
                            providedDraggable2: DraggableProvided,
                            snapshotDraggable2: DraggableStateSnapshot
                          ) => (
                            <div>
                              <div
                                ref={providedDraggable2.innerRef}
                                {...providedDraggable2.draggableProps}
                                {...providedDraggable2.dragHandleProps}
                                style={getItemStyle(
                                  providedDraggable2.draggableProps.style,
                                  snapshotDraggable2.isDragging,
                                  this.state.winnerId === parseInt(item.id)
                                )}
                              >
                                <Header as="h2">
                                  <Image circular src={item.image} />{" "}
                                  {item.content}
                                  <Progress
                                    style={{ marginTop: "10px" }}
                                    indicating
                                    percent={
                                      parseInt(item.id) ===
                                      this.state.opponentId
                                        ? this.state.opponentHP
                                        : this.state.attackerHP
                                    }
                                  />
                                </Header>
                              </div>
                              {providedDraggable2.innerRef}
                            </div>
                          )}
                        </Draggable>
                      ))}
                      {providedDroppable2.placeholder}
                    </div>
                  )}
                </Droppable>
              </Grid.Column>
            </DragDropContext>
            <Grid.Column width={8}>
              <FightResult fightDetails = {this.state.fightAttackDetails}/>
            </Grid.Column>
          </Grid.Row>
          <Grid.Row  textAlign={"justified"}>
            <Grid.Column width={16}>
            <Button.Group widths='2' >
            <Button icon="random" content="Fight" onClick={this.handleFight} disabled={this.state.btnFightDisabled} positive/>
            <Button.Or/>
            <Button
          icon="refresh"
          content="Reset"
          onClick={this.handleReset}
        />
        </Button.Group>
            </Grid.Column>
          </Grid.Row>
        </Grid>
      
      </div>
    );
  }
}
