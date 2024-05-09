class Main extends React.Component {
    render() {
        return (
            <div className="main">
                React component mounted! ^_^
                <ReferencesList></ReferencesList>
            </div>
        );
    }
}

ReactDOM.render(<Main />, document.getElementById('root-react-shortings'));