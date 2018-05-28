from .models import User, get_todays_recent_posts
from flask import Flask, request, session, jsonify
from json import dumps

app = Flask(__name__)


@app.route('/', methods=['GET'])
def index():
    posts = get_todays_recent_posts().data()
    return dumps(posts)


@app.route('/register', methods=['POST'])
def register():
    username = request.json['username']
    password = request.json['password']
    user = User(username).register(password)

    if len(username) < 1 or len(password) < 5 or not user:
        return dumps({'response': 'A user with that username already exists.'})
    else:
        session['username'] = username
        return dumps(user)


@app.route('/login', methods=['POST'])
def login():
    username = request.json['username']
    password = request.json['password']

    if not User(username).verify_password(password):
        return dumps({'response': 'Invalid login.'})
    else:
        session['username'] = username
        return dumps(User(username).find())


@app.route('/logout', methods=['POST'])
def logout():
    session.pop('username', None)
    return dumps({'response': 'logged out'})


@app.route('/add_post', methods=['POST'])
def add_post():
    title = request.json['title']
    tags = request.json['tags']
    text = request.json['text']

    if not title:
        return dumps({'response': 'You must give your post a title.'})
    elif not tags:
        return dumps({'response': 'You must give your post at least one tag.'})
    elif not text:
        return dumps({'response': 'You must give your post a text body.'})
    else:
        try:
            username = session.get('username')
            if username is None:
                username = 'val'
            post = User(username).add_post(title, tags, text)
            return dumps(post)
        except Exception as e:
            return dumps({'response': "Can't create a post %s" % e})


@app.route('/like_post/<post_id>', methods=['PUT'])
def like_post(post_id):
    username = session.get('username')

    if not username:
        username = 'val'

    res = User(username).like_post(post_id)
    print(res)
    res = 'User {} liked post {}'.format(username, post_id) if res else 'Error while liking'
    return dumps({'result': res})


@app.route('/profile/<username>/similar', methods=['GET'])
def profile(username):
    logged_in_username = session.get('username')
    user_being_viewed_username = username

    user_being_viewed = User(user_being_viewed_username)
    posts = user_being_viewed.get_recent_posts().data()

    similar = []
    common = []

    if logged_in_username:
        logged_in_user = User(logged_in_username)

        if logged_in_user.username == user_being_viewed.username:
            similar = logged_in_user.get_similar_users()
        else:
            common = logged_in_user.get_commonality_of_user(user_being_viewed)

    return dumps({'username': username, 'posts': posts, 'similar': similar, 'common': common})


@app.route('/profile/<username>/delete', methods=['DELETE'])
def delete_profile(username):
    logged_in_username = session.get('username')
    #if username != logged_in_username:
    #    return dumps({'response': 'command is prohibited!'})
    #else:
    operation = User(username).__delete__(User(username))
    session.pop(username, None)
    return dumps({'response': 'Deleted user {}'.format(username)})