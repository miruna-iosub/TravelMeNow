import matplotlib.pyplot as plt

plt.xlabel('Model')
plt.ylabel('Accuracy')
plt.title('Test dataset results')

models = ['ResNet50', 'VGG19', 'DenseNet', 'InceptionV3', 'Custom']
acc = [0.9976, 0.9993, 1.0, 1.0, 1.0]
bar_colors = ['tab:purple', 'tab:cyan', 'tab:olive', 'tab:green', 'tab:blue']

plt.ylim(min(acc) - 0.005, max(acc))
plt.bar(models, acc, color=bar_colors)

plt.show()


# plt.xlabel('Seconds')
# plt.ylabel('Model')
# plt.title('Prediction time')
#
# models = ['DenseNet', 'InceptionV3', 'Custom']
# bar_colors = ['tab:olive', 'tab:green', 'tab:blue']
# speed = [0.6340, 0.7856, 0.2134]
# plt.barh(models, speed, align='center', color=bar_colors)
# plt.savefig('../results/pred_time.png')
# plt.show()
