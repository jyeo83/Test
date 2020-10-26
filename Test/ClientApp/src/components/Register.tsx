import * as React from "react";
import { connect, ConnectedProps } from "react-redux";
import * as AuthenticationStore from "../store/Authentication";
import { Modal } from "react-bootstrap";
import { User } from "../store/Authentication";

interface RegisterState {
  userData: User;
}

type OwnProps = {
  toggle: () => void;
  show: boolean;
};

const connector = connect(null, AuthenticationStore.actionCreators);
type ReduxProps = ConnectedProps<typeof connector>;

type RegisterProps = OwnProps & ReduxProps;

class Register extends React.Component<RegisterProps> {
  state: RegisterState = {
    userData: {
      userId: 0,
      employeeNo: "",
      firstName: "",
      middleInitial: "",
      lastName: "",
      email: "",
      password: "",
    },
  };

  handleChange = (e: React.ChangeEvent<HTMLInputElement>) =>
    this.setState({
      userData: { ...this.state.userData, [e.target.name]: e.target.value },
    });

  handleSubmit = (e: React.SyntheticEvent) => {
    e.preventDefault();
    const { userData: user } = this.state;
    this.props.register(user);
    this.props.toggle();
  };

  render() {
    const { show, toggle } = this.props;
    const {
      employeeNo,
      firstName,
      middleInitial,
      lastName,
      email,
      password,
    } = this.state.userData;
    return (
      <Modal show={show} onHide={toggle}>
        <Modal.Header closeButton>
          <Modal.Title>Log In</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <div className="row mb-1">
            <div className="form-group col-12">
              <label htmlFor="employeeNo">
                <b>Username:</b>
              </label>
              <input
                type="text"
                className="form-control"
                name="employeeNo"
                id="employeeNo"
                value={employeeNo}
                onChange={this.handleChange}
              />
            </div>
          </div>
          <div className="row mb-1">
            <div className="form-group col-12">
              <label htmlFor="firstName">
                <b>First Name:</b>
              </label>
              <input
                type="text"
                className="form-control"
                name="firstName"
                id="firstName"
                value={firstName}
                onChange={this.handleChange}
              />
            </div>
          </div>
          <div className="row mb-1">
            <div className="form-group col-12">
              <label htmlFor="lastName">
                <b>Last Name:</b>
              </label>
              <input
                type="text"
                className="form-control"
                name="lastName"
                id="lastName"
                value={lastName}
                onChange={this.handleChange}
              />
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
              />
            </div>
          </div>
        </Modal.Body>
        <Modal.Footer>
          <div className="form-row form-group">
            <button
              type="button"
              name="register"
              id="register"
              className="btn btn-primary mr-2"
              onClick={this.handleSubmit}
            >
              Register
            </button>
            <button
              type="button"
              name="cancelRegister"
              id="cancelRegister"
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

export default connector(Register);
