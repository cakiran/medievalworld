export interface IFightRequestDto {
  fighters: number[];
}

export interface IUserDetail {
  username: string;
  password: string;
}

export interface INavBarProps {
  setUserId: (userId: number) => void;
}

export interface IFightArenaProps {
  userId: number;
}

export interface IAddSelectedFightersDto {
  selectedFighters: ISelectedFighter[];
}

export interface ISelectedFighter {
  id: number;
  userId: number;
  content: string;
}

export interface Item {
  id: string;
  content: string;
  image: any;
  userId: number;
}

export interface IAppState {
  items: Item[];
  selected: Item[];
  fightTally: IFightTallyDetails[];
  fightResult: [];
  fightAttackDetails: IFightDetails[];
  opponentId: number;
  opponentHP: number;
  winnerId: number;
  attackerHP: number;
  userId: number;
  isUnlocked:boolean;
  btnFightDisabled:boolean;
}

export interface IMoveResult {
  droppable: Item[];
  droppable2: Item[];
}

export interface IFightDetails {
  opponentId: number;
  opponentName: string;
  logText: string;
  winner: string;
  loser: string;
  opponentHP: number;
  attackerHP: number;
  winnerId: number;
}

export type FormData = {
    username: string;
    password: string;
  }
  
 export type RegisterFormData = {
    username: string;
    password: string;
    password1: string;
  }

  export interface IFightTallyDetails {
   username:string;
    fights:number;
    victories:number;
   defeats:number;
  }
    
  export interface IFightTallyProps {
    fightTally:IFightTallyDetails[]
  }
  export interface IFightDetailsProps {
    fightDetails:IFightDetails[]
  }