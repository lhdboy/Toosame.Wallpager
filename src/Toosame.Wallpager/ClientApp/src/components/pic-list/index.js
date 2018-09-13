import React, { Component } from 'react';
import { Picture } from '../pic/index';
import { Grid, Row, Col, Modal, Breadcrumb, Carousel, Button } from 'react-bootstrap';
import { TagButton } from '../tag-button/index';
import history from '../../history';

export class PictureGroup extends Component {
    displayName = PictureGroup.name

    constructor() {
        super();
        this.hidePictureDetail = this.hidePictureDetail.bind(this);
        this.getDetail = this.getDetail.bind(this);
        this.state = {
            detailPic: null,
            detailPicName: '',
            detailPicSrc: [],
            detailPicTags: [],
            detailPicIntro: '',
            detailPicNum: '',
            detailPicSize: '',
            detailPicTypeName: '',
            detailPicChannelName: '',
            detailShow: false
        };
    }

    getDetail(pic) {
        fetch(`/api/picture/${pic.picId}`)
            .then(res => res.json())
            .then(json => {
                this.setState({
                    detailShow: true,
                    detailPic: json,
                    detailPicName: json.picName,
                    detailPicNum: json.picNum,
                    detailPicIntro: json.intro,
                    detailPicSize: json.picSize,
                    detailPicSrc: json.images,
                    detailPicTags: json.tags,
                    detailPicTypeName: json.typeName,
                    detailPicChannelName: json.channelName,
                    downloadUrl: '',
                });
            }).catch(e => console.log(e));
    }

    hidePictureDetail() {
        this.setState({ detailShow: false });
    }

    render() {
        const dataSource = this.props.data;
        let gridList = [];
        let gridRowIndex = -1;
        let gridColIndex = 0;

        if (dataSource !== undefined && dataSource !== null) {
            dataSource.forEach((v, i) => {
                if (i % 3 === 0) {
                    gridRowIndex++;
                    gridColIndex = 0;

                    gridList[gridRowIndex] = [];
                    gridList[gridRowIndex][gridColIndex] = v;

                    gridColIndex++;
                } else {
                    gridList[gridRowIndex][gridColIndex] = v;

                    gridColIndex++;
                }
            });
        }
        return (
            <div>
                {gridList.map((v, i) =>
                    <Grid key={i}>
                        <Row>
                            {v.map((c, k) =>
                                <Col key={k} md={4} style={{ cursor: 'pointer' }} onClick={() => this.getDetail(c)}>
                                    <Picture data={c} />
                                </Col>)}
                        </Row>
                    </Grid>
                )}
                <a href={this.state.downloadUrl} id='downloadUrl' target="_black" style={{ display: 'none' }}></a>
                <Modal
                    {...this.props}
                    show={this.state.detailShow}
                    onHide={this.hidePictureDetail}
                    dialogClassName="custom-modal"
                >
                    <Modal.Header closeButton>
                        <Modal.Title id="contained-modal-title-lg">{this.state.detailPicName}</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Carousel>
                            {this.state.detailPicSrc.map((v, i, a) =>
                                <Carousel.Item>
                                    <img
                                        onClick={() => this.setState({ downloadUrl: v.url }, () => document.getElementById('downloadUrl').click())}
                                        style={{ cursor: 'pointer', width: '100%' }}
                                        alt={this.state.detailPicSize}
                                        src={v.preview} />
                                </Carousel.Item>)}
                        </Carousel>
                        <p style={{ marginTop: 12, color: '#717171' }}>点击图片即可下载，如无法下载请在弹出的新标签页右键图片另存为</p>
                        <Breadcrumb style={{ marginTop: 8 }}>
                            <Breadcrumb.Item
                                onClick={() => history.push({
                                    pathname: `/catalog`,
                                    state: {
                                        name: this.state.detailPicTypeName
                                    },
                                })}>
                                {this.state.detailPicTypeName}
                            </Breadcrumb.Item>
                            <Breadcrumb.Item
                                onClick={() => history.push({
                                    pathname: `/channel/${this.state.detailPic.channelId}`,
                                    state: {
                                        name: this.state.detailPicChannelName,
                                    },
                                })}>
                                {this.state.detailPicChannelName}
                            </Breadcrumb.Item>
                        </Breadcrumb>
                        <p>{this.state.detailPicNum}张 · <span style={{ color: '#7d7d7d' }}>{this.state.detailPicSize}</span></p>
                        <p>{this.state.detailPicIntro}</p>
                        <TagButton tags={this.state.detailPicTags} />
                    </Modal.Body>
                    <Modal.Footer>
                        <Button onClick={this.hidePictureDetail} bsStyle="success">关闭</Button>
                    </Modal.Footer>
                </Modal>
            </div>
        );
    }
}
