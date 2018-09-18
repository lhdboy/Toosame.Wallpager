import React, { Component } from 'react';
import { Grid, Col, Row, Carousel, Breadcrumb, Button, Glyphicon } from 'react-bootstrap';
import { TagButton } from '../../components/tag-button/index';
import { PictureGroup } from '../../components/pic-list/index';
import history from '../../history';

export class Home extends Component {
    displayName = Home.name

    constructor() {
        super();
        this.getTodayPicture = this.getTodayPicture.bind(this);
        this.getRecommend = this.getRecommend.bind(this);
        this.getTags = this.getTags.bind(this);
        this.state = {
            todayPicture: null,
            todayPictureSrc: [],
            todayPictureName: null,
            todayPictureSize: null,
            todayPictureNum: null,
            todayPictureTypeName: null,
            todayPictureChannelName: null,
            todayPictureTags: [],
            randomPictures: [],
            randomTags: [],
            downloadUrl: '',
        };
    }

    componentDidMount() {
        this.getTodayPicture();
        this.getRecommend();
        this.getTags();
    }

    getTodayPicture() {
        fetch(`/api/picture`)
            .then(res => res.json())
            .then(json => {
                this.setState({
                    todayPicture: json,
                    todayPictureSrc: json.images,
                    todayPictureName: json.picName,
                    todayPictureNum: json.images.length,
                    todayPictureIntro: json.intro,
                    todayPictureTypeName: json.typeName,
                    todayPictureChannelName: json.channelName,
                    todayPictureTags: json.tags
                });
            })
            .catch(e => {
                console.log(e);
            });
    }

    getRecommend() {
        fetch(`/api/picture/recommend?count=24`)
            .then(res => res.json())
            .then(json => {
                window.scroll({ top: 0 });
                this.setState({
                    randomPictures: json,
                });
            })
            .catch(e => {
                console.log(e);
            });
    }

    getTags() {
        fetch(`/api/tag/get?count=24`)
            .then(res => res.json())
            .then(json => {
                this.setState({
                    randomTags: json,
                });
            })
            .catch(e => {
                console.log(e);
            });
    }

    render() {
        return (
            <Grid>
                <h2>今日推荐</h2>
                <Row>
                    <a href={this.state.downloadUrl} id='downloadPicUrl' target="_black" style={{ display: 'none' }}></a>
                    <Col md={8}>
                        <Carousel>
                            {this.state.todayPictureSrc.map((v, i, a) =>
                                <Carousel.Item key={v.preview}>
                                    <img
                                        onClick={() => this.setState({ downloadUrl: v.url }, () => document.getElementById('downloadPicUrl').click())}
                                        style={{ cursor: 'pointer', width: '100%' }}
                                        alt={this.state.todayPictureSize}
                                        src={v.preview} />
                                </Carousel.Item>)}
                        </Carousel>
                    </Col>
                    <Col md={4} style={{ overflowY: 'hidden' }}>
                        <h3>{this.state.todayPictureName}</h3>
                        <Breadcrumb>
                            <Breadcrumb.Item
                                onClick={() => history.push({
                                    pathname: `/catalog`,
                                    state: {
                                        name: this.state.todayPictureTypeName
                                    },
                                })}>
                                {this.state.todayPictureTypeName}
                            </Breadcrumb.Item>
                            <Breadcrumb.Item
                                onClick={() => history.push({
                                    pathname: `/channel/${this.state.todayPicture.channelId}`,
                                    state: {
                                        name: this.state.todayPictureChannelName,
                                    },
                                })}>
                                {this.state.todayPictureChannelName}
                            </Breadcrumb.Item>
                        </Breadcrumb>
                        <p>{this.state.todayPictureIntro}</p>
                        <TagButton tags={this.state.todayPictureTags} />
                    </Col>
                </Row>
                <h2 style={{ marginTop: 38 }}>热门标签</h2>
                <TagButton tags={this.state.randomTags} />
                <h2 style={{ marginTop: 38 }}>猜你喜欢</h2>
                {this.state.randomPictures.length > 0
                    ? <PictureGroup data={this.state.randomPictures} />
                    : <div style={{ textAlign: 'center', paddingTop: 50, paddingBottom: 50 }}>
                        <h2><Glyphicon glyph="glyphicon glyphicon-inbox" />&nbsp;&nbsp;&nbsp;&nbsp;暂无数据</h2>
                    </div>
                }
                <Button bsStyle="primary" bsSize="large" block onClick={this.getRecommend}>
                    <Glyphicon glyph="glyphicon glyphicon-refresh" />&nbsp;&nbsp;&nbsp;&nbsp;换一组
                </Button>
            </Grid>
        );
    }
}
