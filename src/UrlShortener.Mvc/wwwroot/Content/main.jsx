class ReferencesList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            list: []
        }
    }
    componentDidMount() {
        axios.get("/api/Shorten/GetAllUrls").then((result) => {
            this.setState({ list: result.data });
        });
    }
    render() {
        const viewData = this.state.list.map((url) => (
            <tr key={url.shortenedUrl}>
                <td>{url.shortenedUrl}</td>
                <td>{url.fullUrl}</td>
                <td>{url.createdById}</td>
            </tr>
        ));
        console.log(this.state.list, viewData);

        return (<>
            <div className="onLoad">
                <a href="/admin/category/create" className="btn btn-success">
                    Add
                </a>
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