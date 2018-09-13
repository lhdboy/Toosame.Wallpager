import React, { Component } from 'react';
import { Grid, PageHeader, Pager, Glyphicon } from 'react-bootstrap';
import { PictureGroup } from '../../components/pic-list/index';

export class Search extends Component {
    displayName = Search.name

    constructor() {
        super();
        this.search = this.search.bind(this);
        this.reset = this.reset.bind(this);
        this.next = this.next.bind(this);
        this.previous = this.previous.bind(this);
        this.state = {
            isFirst: true,
            pageIndex: 1,
            pageSize: 24,
            data: [],
        };
    }

    componentDidMount() {
        this.search(this.props.location.state.keyword);
    }

    componentWillReceiveProps(nextProps, nextState) {
        if (nextProps.location.state.keyword !== this.props.location.state.keyword && !this.state.isFirst) {
            this.reset(nextProps.location.state.keyword);
            return true;
        }
    }

    search(keyword) {
        if (keyword === undefined) {
            keyword = this.props.location.state.keyword;
        }

        fetch(`/api/picture/search?keyword=${encodeURI(keyword)}&index=${this.state.pageIndex}&size=${this.state.pageSize}`)
            .then(res => res.json())
            .then(json => {
                this.setState({
                    data: json,
                    isFirst: false,
                });
            })
            .catch(e => {
                console.log(e);
            });
    }

    next() {
        this.setState({
            pageIndex: this.state.pageIndex += 1,
        }, () => this.search());
    }

    previous() {
        this.setState({
            pageIndex: this.state.pageIndex -= 1,
        }, () => this.search());
    }

    reset(keyword) {
        this.setState({
            pageIndex: 1,
            pageSize: 24,
            data: [],
        }, () => this.search(keyword));
    }

    render() {
        return (
            <Grid>
                <PageHeader>
                    {this.props.location.state.keyword}&nbsp;&nbsp;<small>的搜索结果</small>
                </PageHeader>
                {this.state.data.length > 0
                    ? <PictureGroup data={this.state.data} />
                    : <div style={{ textAlign: 'center', paddingTop: 25, paddingBottom: 25 }}>
                        <h2><Glyphicon glyph="glyphicon glyphicon-inbox" />&nbsp;&nbsp;&nbsp;&nbsp;没有搜索到任何结果</h2>
                      </div>
                }
                <Pager>
                    <Pager.Item previous disabled={this.state.pageIndex <= 1} onClick={this.previous}>上一页</Pager.Item>
                    <span>第 {this.state.pageIndex} 页</span>
                    <Pager.Item next disabled={this.state.data.length < 24} onClick={this.next}>下一页</Pager.Item>
                </Pager>
            </Grid>
        );
    }
}