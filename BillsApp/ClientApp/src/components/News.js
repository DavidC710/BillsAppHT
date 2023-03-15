import React, { Component } from 'react';

export class News extends Component {
    static displayName = News.name;

    constructor(props) {
        super(props);
        this.state = { news: [], loading: true };
    }

    componentDidMount() {
        this.getNews();
    }

    static renderNews(news) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Date</th>
                        <th>Temp. (C)</th>
                        <th>Temp. (F)</th>
                        <th>Summary</th>
                    </tr>
                </thead>
                <tbody>
                    {news.map(n =>
                        <tr key={n.date}>
                            <td>{n.date}</td>
                            <td>{n.temperatureC}</td>
                            <td>{n.temperatureF}</td>
                            <td>{n.summary}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : News.renderNews(this.state.news);

        return (
            <div>
                <h1 id="tabelLabel" >News!</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    async getNews() {
        const response = await fetch('weatherforecast');
        const data = await response.json();
        this.setState({ news: data, loading: false });
    }
}
