import * as React from "react";
import { connect, ConnectedProps } from "react-redux";
import { ApplicationState } from "../store";
import * as AuthenticationStore from "../store/Authentication";
import { Modal } from "react-bootstrap";

interface LoginState {
  username: string;
  password: string;
  isChanged: boolean;
  processing: boolean;
}

type OwnProps = {
  toggle: () => void;
  show: boolean;
};

type StateProps = {
  user?: AuthenticationStore.User;
};

const mapState = (state: ApplicationState) => ({
  user: state.authentication ? state.authentication.user || undefined : undefined,
});
const connector = connect(mapState, AuthenticationStore.actionCreators);
type ReduxProps = ConnectedProps<typeof connector>;

type LoginProps = OwnProps & StateProps & ReduxProps;

class Login extends React.Component<LoginProps> {
  state: LoginState = {
    username: "",
    password: "",
    isChanged: false,
    processing: false,
  };

  handleChange = (e: React.ChangeEvent<HTMLInputElement>): void =>
    this.setState({ ...this.state, [e.target.name]: e.target.value, isChanged: true });

  handleSubmit = (e: React.SyntheticEvent): void => {
    e.preventDefault();
    const { username, password } = this.state;
    this.setState({ ...this.state, processing: true });
    const user: AuthenticationStore.User = {
      userId: 0,
      employeeNo: username,
      firstName: "",
      middleInitial: "",
      lastName: "",
      email: "",
      password,
    };
    this.props.authenticate();
    this.props.toggle();
  };

  render() {
    const { show, toggle } = this.props;
    const { username, password, isChanged, processing } = this.state;
    return (
      <Modal show={show} onHide={toggle}>
        <Modal.Header closeButton>
          <Modal.Title>Log In</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <div className="row">
            <div className="form-group col-12">
              <label htmlFor="username">
                <b>Username:</b>
              </label>
              <input
                type="text"
                className="form-control"
                name="username"
                id="username"
                value={username}
                onChange={this.handleChange}
                disabled={processing}
              />
              {isChanged && !username && (
                <span className="mt-0 text-danger">
                  <small>Username is required</small>
                </span>
              )}
            </div>
          </div>
          <div className="row">
            <div className="form-group col-12">
              <label htmlFor="password">
                <b>Password:</b>
              </label>
              <input
                type="password"
                className="form-control"
                name="password"
                value={password}
                onChange={this.handleChange}
                onKeyDown={(e) => e.key === "Enter" && this.handleSubmit(e)}
                disabled={processing}
              />
              {isChanged && !password && (
                <span className="mt-0 text-danger">
                  <small>Password is required</small>
                </span>
              )}
            </div>
          </div>
        </Modal.Body>
        <Modal.Footer>
          <div className="form-row form-group">
            <button
              type="button"
              name="login"
              id="login"
              className="btn btn-primary mr-2"
              onClick={this.handleSubmit}
            >
              Log In
            </button>
            <button
              type="button"
              name="cancelLogin"
              id="cancelLogin"
              className="btn btn-secondary"
              onClick={toggle}
            >
              Cancel
            </button>
          </div>
        </Modal.Footer>
      </Modal>
    );
  }
}

export default connector(Login);
