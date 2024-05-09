class ReferencesList extends React.Component {
    constructor(props) {
        super(props);
        this.state = { list: [] }
    }
    componentWillMount() {
        var result = axios.get("/");
        console.log(result);
    }
    render() {
        return (
            <div className="onLoad">
                {/*<Link to="/api/Shorten/ShortenUrl" className="btn btn-success">*/}
                {/*Add*/}
                {/*</Link>*/}
                <table className="table">
                    <thead>
                        <tr>
                            <th scope="col">Id</th>
                            <th scope="col" style={{ width: "20vw" }}>
                                Назва
                            </th>
                            <th scope="col">Фото</th>
                            <th scope="col"></th>
                        </tr>
                    </thead>
                    {/*<tbody>{viewData}</tbody>*/}
                </table>
                <ul className="pagination justify-content-center">
                    {/*{paginationData}*/}
                </ul>
            </div>
        );
    }
}