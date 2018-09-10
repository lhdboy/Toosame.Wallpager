import React, { Component } from 'react';
import { Grid, Col, Row, Carousel, Breadcrumb } from 'react-bootstrap';
import { TagButton } from '../../components/tag-button/index';

export class Home extends Component {
    displayName = Home.name

    constructor() {
        super();
        this.getTodayPicture = this.getTodayPicture.bind(this);
        this.state = {
            todayPictureSrc: [],
            todayPictureName: null,
            todayPictureSize: null,
            todayPictureNum: null,
            todayPictureTypeName: null,
            todayPictureChannelName: null,
            todayPictureTags: [],
        };
    }

    componentDidMount() {
        this.getTodayPicture();
    }

    getTodayPicture() {
        fetch(`/api/picture`)
            .then(res => res.json())
            .then(json => {
                this.setState({
                    todayPictureSrc: json.images,
                    todayPictureName: json.name,
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

    render() {
        return (
            <Grid>
                <Row>
                    <Col md={8}>
                        <Carousel>
                            {this.state.todayPictureSrc.map((v, i, a) =>
                                <Carousel.Item>
                                    <img style={{ width: '100%' }} alt={this.state.todayPictureSize} src={v.preview} />
                                </Carousel.Item>)}
                        </Carousel>
                    </Col>
                    <Col md={4} style={{ overflowY: 'hidden' }}>
                        <h3>{this.state.todayPictureName}</h3>
                        <Breadcrumb>
                            <Breadcrumb.Item href="#">{this.state.todayPictureTypeName}</Breadcrumb.Item>
                            <Breadcrumb.Item href="#">{this.state.todayPictureChannelName}</Breadcrumb.Item>
                        </Breadcrumb>
                        <p>{this.state.todayPictureIntro}</p>
                        <TagButton tags={this.state.todayPictureTags} />
                    </Col>
                </Row>
            </Grid>
        );
    }
}
