import * as React from "react";
import { connect, ConnectedProps } from "react-redux";
import Login from "./Login";
import Register from "./Register";
import * as AuthenticationStore from "../store/Authentication";
import { ApplicationState } from "../store";
import BASE_URL from "../constants/urls";

interface HomeState {
  showModal_login: boolean;
  showModal_register: boolean;
}

type StateProps = {
  user?: AuthenticationStore.User;
};

const mapState = (state: ApplicationState) => ({
  user: state.authentication ? state.authentication.user : undefined,
});
const connector = connect(mapState, AuthenticationStore.actionCreators);
type ReduxProps = ConnectedProps<typeof connector>;

type HomeProps = StateProps & ReduxProps;

class Home extends React.PureComponent<HomeProps> {
  state: HomeState = {
    showModal_login: false,
    showModal_register: false,
  };

  private handleClick_register = (e: React.SyntheticEvent) => {
    e.preventDefault();
    this.toggleModal_register();
  };

  private handleClick_login = (e: React.SyntheticEvent) => {
    e.preventDefault();
    this.props.authenticate();
    // this.toggleModal_login();
  };

  private handleClick_testAuth = (e: React.SyntheticEvent) => {
    e.preventDefault();
    this.props.testAuth();
  };

  private toggleModal_login = () =>
    this.setState({ showModal_login: !this.state.showModal_login });

  private toggleModal_register = () =>
    this.setState({ showModal_register: !this.state.showModal_register });

  public render() {
    const { user } = this.props;

    return (
      <React.Fragment>
        <div>
          <h1>Hello, {user ? user.firstName : "World"}!</h1>
          <p>Welcome to your new single-page application, built with:</p>
          <ul>
            <li>
              <a href="https://get.asp.net/">ASP.NET Core</a> and{" "}
              <a href="https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx">C#</a> for
              cross-platform server-side code
            </li>
            <li>
              <a href="https://facebook.github.io/react/">React</a> and{" "}
              <a href="https://redux.js.org/">Redux</a> for client-side code
            </li>
            <li>
              <a href="http://getbootstrap.com/">Bootstrap</a> for layout and styling
            </li>
          </ul>
          <p>
            <button
              type="button"
              name="register"
              id="register"
              className="btn btn-secondary mr-2"
              onClick={this.handleClick_register}
            >
              Register
            </button>
            <a
              href={BASE_URL + "/Saml/InitiateSingleSignOn"}
              className="btn btn-secondary mr-2"
            >
              Log In
            </a>
            {/* <button
              type="button"
              name="login"
              id="login"
              className="btn btn-secondary mr-2"
              onClick={this.handleClick_login}
            >
              Log In
            </button> */}
            <button
              type="button"
              name="authAction"
              id="authAction"
              className="btn btn-danger"
              onClick={this.handleClick_testAuth}
            >
              Test Auth
            </button>
          </p>
          <div id="testauth"></div>
          <p>To help you get started, we've also set up:</p>
          <ul>
            <li>
              <strong>Client-side navigation</strong>. For example, click <em>Counter</em>{" "}
              then <em>Back</em> to return here.
            </li>
            <li>
              <strong>Development server integration</strong>. In development mode, the
              development server from <code>create-react-app</code> runs in the background
              automatically, so your client-side resources are dynamically built on demand
              and the page refreshes when you modify any file.
            </li>
            <li>
              <strong>Efficient production builds</strong>. In production mode,
              development-time features are disabled, and your <code>dotnet publish</code>{" "}
              configuration produces minified, efficiently bundled JavaScript files.
            </li>
          </ul>
          <p>
            The <code>ClientApp</code> subdirectory is a standard React application based
            on the <code>create-react-app</code> template. If you open a command prompt in
            that directory, you can run <code>npm</code> commands such as{" "}
            <code>npm test</code> or <code>npm install</code>.
          </p>
        </div>
        {this.state.showModal_login && (
          <Login toggle={this.toggleModal_login} show={this.state.showModal_login} />
        )}
        {this.state.showModal_register && (
          <Register
            toggle={this.toggleModal_register}
            show={this.state.showModal_register}
          />
        )}
      </React.Fragment>
    );
  }
}

export default connector(Home);
