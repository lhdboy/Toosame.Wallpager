import React, { Component } from 'react';
import { ButtonToolbar, Button } from 'react-bootstrap';

export class TagButton extends Component {
    displayName = TagButton.name

    getStyleByRandom() {
        const styleList = ["primary", "success", "info", "warning", "danger"];

        let randomIndex = 0;
        do {
            randomIndex = Math.floor(Math.random() * 10);
        } while (styleList.length < randomIndex);

        return styleList[randomIndex];
    }

    render() {
        return (
            <ButtonToolbar>
                {this.props.tags.map((v, i, a) => <Button style={{ marginTop: 5 }} bsStyle={this.getStyleByRandom()}>{v.tagName}</Button>)}
            </ButtonToolbar>
        );
    }
}
