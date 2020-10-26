import { Action, Reducer } from "redux";
import { AppThunkAction } from "./";
import axios, { AxiosResponse } from "axios";
import BASE_URL from "../constants/urls";

export interface User {
  userId: number;
  employeeNo: string;
  firstName: string;
  middleInitial: string;
  lastName: string;
  email: string;
  password?: string;
}

// STATE
export interface AuthenticationState {
  user?: User;
  loggedInOn?: Date;
  isLoading: boolean;
}

// ACTIONS
interface RegisterRequestAction {
  type: "REGISTER_REQUEST";
}

interface RegisterSuccessAction {
  type: "REGISTER_SUCCESS";
  user: User;
}

interface AuthenticateRequestAction {
  type: "AUTHENTICATE_REQUEST";
}

interface AuthenticateSuccessAction {
  type: "AUTHENTICATE_SUCCESS";
  user: User;
}

interface TestAuthRequestAction {
  type: "TEST_AUTH_REQUEST";
}

type KnownAction =
  | RegisterRequestAction
  | RegisterSuccessAction
  | AuthenticateRequestAction
  | AuthenticateSuccessAction
  | TestAuthRequestAction;

// ACTION CREATOR
export const actionCreators = {
  register: (user: User): AppThunkAction<KnownAction> => (dispatch) => {
    dispatch({ type: "REGISTER_REQUEST" });
    const apiEndpoint = BASE_URL + "/api/User/Create";
    axios.post(apiEndpoint, user).then((response: AxiosResponse) => {
      dispatch({ type: "REGISTER_SUCCESS", user: response.data });
    });
  },
  authenticate: (): AppThunkAction<KnownAction> => (dispatch) => {
    dispatch({ type: "AUTHENTICATE_REQUEST" });
    const apiEndpoint = BASE_URL + "/Saml/InitiateSingleSignOn";
    axios.get(apiEndpoint).then((response: AxiosResponse) => {
      console.log("logged in:", response.data);
      dispatch({ type: "AUTHENTICATE_SUCCESS", user: response.data });
    });
  },
  testAuth: (): AppThunkAction<KnownAction> => (dispatch) => {
    dispatch({ type: "TEST_AUTH_REQUEST" });
    const apiEndpoint = BASE_URL + "/api/User/Test";
    axios
      .get(apiEndpoint)
      .then((response) => {
        console.log("success", response);
        const elem = document.getElementById("testauth");
        console.log("elem=", elem);
        if (elem) elem.innerText = response.data;
      })
      .catch((err) => console.log("not allowed = ", err));
  },
};

// REDUCER
const unloadedState: AuthenticationState = { user: {} as User, isLoading: false };

export const reducer: Reducer<AuthenticationState> = (
  state: AuthenticationState | undefined,
  incomingAction: Action
): AuthenticationState => {
  if (state === undefined) return unloadedState;
  const action = incomingAction as KnownAction;
  switch (action.type) {
    case "REGISTER_REQUEST":
      return { ...state, isLoading: true };
    case "REGISTER_SUCCESS":
      return { ...state, user: action.user, isLoading: false };
    case "AUTHENTICATE_REQUEST":
      return { ...state, isLoading: false };
    case "AUTHENTICATE_SUCCESS":
      return { ...state, user: action.user, loggedInOn: new Date(), isLoading: false };
    case "TEST_AUTH_REQUEST":
      return { ...state };
    default:
      return state;
  }
};
