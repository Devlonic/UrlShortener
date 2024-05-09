function getCookie(name) {
    const cookies = document.cookie.split(';');
    for (let i = 0; i < cookies.length; i++) {
        const cookie = cookies[i].trim();
        if (cookie.startsWith(name + '=')) {
            return cookie.substring(name.length + 1);
        }
    }
    return null;
}

class AddNewReference extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            fullLink: ''
        }

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(event) {
        this.setState({ fullLink: event.target.value });
    }

    handleSubmit(event) {
        event.preventDefault();
        console.log(event, { fullLink: this.state.fullLink });

        axios.post("/api/Shorten/ShortenUrl", this.state).then((result) => {
            this.props.onAdd(result.data);
        });
    }

    render() {
        return <>
            <form className="mb-3" onSubmit={this.handleSubmit}>
                <div>
                    <h2>Add shortcut</h2>
                </div>
                <div className="d-flex">
                    <div className="d-flex mr-3">
                        <input onChange={this.handleChange} type="url" className="form-control" id="fullLink-input" name="fullLink"></input>
                    </div>
                    <button type="submit" className="btn btn-primary">Add</button>
                </div>
            </form >
        </>
    }
}

class ReferencesList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            list: []
        }
        this.addRef = this.addRef.bind(this);

    }
    componentDidMount() {
        axios.get("/api/Shorten/GetAllUrls").then((result) => {
            this.setState({ list: result.data });
        });
    }

    addRef(ref) {
        console.log("added", ref);
        this.setState({ list: [...this.state.list, ref] });
    }

    render() {
        const viewData = this.state.list.map((url) => (
            <tr key={url.shortenedUrl}>
                <td>{url.shortenedUrl}</td>
                <td>{url.fullUrl}</td>
                <td>{url.createdById}</td>
                <td>
                    <button className="btn btn-danger">Remove</button>
                </td>
            </tr>
        ));
        console.log(this.state.list, viewData);

        return (<>
            <div className="onLoad">
                {getCookie("Authentication") && <>
                    <AddNewReference onAdd={this.addRef}></AddNewReference>
                </>}

                <table className="table">
                    <thead>
                        <tr>
                            <th scope="col">Short hash</th>
                            <th scope="col" style={{ width: "20vw" }}>
                                Forward to
                            </th>
                            <th scope="col">Creator Id</th>
                            <th scope="col"></th>
                        </tr>
                    </thead>
                    <tbody>{viewData}</tbody>
                </table>
                <ul className="pagination justify-content-center">
                    {/*{paginationData}*/}
                </ul>
            </div>
        </>);
    }
}
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