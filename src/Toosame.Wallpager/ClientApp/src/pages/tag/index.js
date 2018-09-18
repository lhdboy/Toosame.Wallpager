import React, { Component } from 'react';
import { Grid, PageHeader, Pager, Glyphicon } from 'react-bootstrap';
import { PictureGroup } from '../../components/pic-list/index';

export class Tag extends Component {
    displayName = Tag.name

    constructor() {
        super();
        this.getTag = this.getTag.bind(this);
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
        this.getTag(this.props.match.params.tag);
    }

    componentWillReceiveProps(nextProps, nextState) {
        if (nextProps.match.params.tag !== this.props.match.params.tag && !this.state.isFirst) {
            this.reset(nextProps.match.params.tag);
            return true;
        }
    }

    getTag(tag) {
        if (tag === undefined) {
            tag = this.props.match.params.tag;
        }

        fetch(`/api/tag/${tag}?index=${this.state.pageIndex}&size=${this.state.pageSize}`)
            .then(res => res.json())
            .then(json => {
                window.scroll({ top: 0 });
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
        }, () => this.getTag());
    }

    previous() {
        this.setState({
            pageIndex: this.state.pageIndex -= 1,
        }, () => this.getTag());
    }

    reset(tag) {
        this.setState({
            pageIndex: 1,
            pageSize: 24,
            data: [],
        }, () => this.getTag(tag));
    }

    render() {
        console.log(this.props.location.state);

        return (
            <Grid>
                <PageHeader>
                    {this.props.location.state.name}&nbsp;&nbsp;<small>标签的图片</small>
                </PageHeader>
                {this.state.data.length > 0
                    ? <PictureGroup data={this.state.data} />
                    : <div style={{ textAlign: 'center', paddingTop: 50, paddingBottom: 50 }}>
                        <h2><Glyphicon glyph="glyphicon glyphicon-inbox" />&nbsp;&nbsp;&nbsp;&nbsp;暂无数据</h2>
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